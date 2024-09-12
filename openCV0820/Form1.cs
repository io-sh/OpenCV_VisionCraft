

using Cognex.InSight;
using Cognex.InSight.Controls.Display;

using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using OpenCvSharp;
using OpenCvSharp.Extensions;


using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;


using System.Windows.Controls.Ribbon;

using System.Linq.Expressions;


using static Cognex.InSight.UserAccess.CvsPermissionDefinitions;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.AxHost;
using static openCV0820.userControl_barcode;
using System.Reflection.Emit;
using ZXing;

//using System.Windows.Media;


namespace openCV0820
{
    public partial class Form1 : RibbonForm
    {
        Mat image;
        Mat bin;
        OpenCV_CLASS convert;
        OpenCV_filter covertfilter;
        OpenCV_Detection detect;
        OpenCV_action openCV_Action;

        VideoCapture video;//카메라 출력
        Mat camframe;

        //자르기crop에 사용할 데이터
        int cropX;
        int cropY;
        int cropWidth;
        int cropHeight;
        int[] cropXY;
        public Pen cropPen;
        public DashStyle cropDashStyle;
        public bool cropSelection = false;

        

        //Cognex카메라
        CvsInSight insight = new CvsInSight();
        bool IsConnected1 = false;
        bool OnLineST1 = false;

        //기하학적 변환
        UserControl_affine userControl_affine1;
        bool affine_flag = false;
        float lux, luy, ldx, ldy, rux, ruy, rdx, rdy;
        int clickCount = 0;
        float[] xy;

        //글자검출
        UserControl_Tesseract userControl_Tesseract;
        bool tesseract_flag = false;

        //이진화
        bool binBTN_flag = false;
        UserControl_Bin userControl_Bin;

        //모폴로지
        bool dileroBTN_flag = false;
        UserControl_DilEro userControl_DilEro;
        Mat de;

        //블러
        bool blur_flag = false;
        UserControl_blur userControl_Blur;
        Mat applyBlur;

        //색상 검출 
        bool color_flag = false;
        UserControl_color userControl_Color;

        //부분 색상 백분율 검출
        bool percentColor_flag = false;

        //코인 검출
        bool coinBTN_flag = false;

        //바코드
        userControl_barcode userControl_Barcode;
        bool barcode_flag = false;
        string company;
        string product;
        string production;
        DateTime date;
        string test;
        int year;
        int month;
        bool luhn;
        DBConnect db = new DBConnect();

        int sum;

        //얼굴 검출
        bool face_flag = false;
        UserControl_face userControl_Face;
        bool deco1 = false;
        bool deco2 = false;
        bool deco3 = false;
        bool deco4 = false;
        bool deco5 = false;
        
        string earImage1 = "../../bin/Debug/haarcascade_frontalface_alt/rabbit_ear.png";//토끼귀
        string girlHairImage = "../../bin/Debug/haarcascade_frontalface_alt/girl_hair.png";
        string baldHeadImage = "../../bin/Debug/haarcascade_frontalface_alt/baldHead.png";
        string sunglassesImage = "../../bin/Debug/haarcascade_frontalface_alt/sunglasses.png";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Mat(640, 480, MatType.CV_8UC3);//초기 이미지 비율
            convert = new OpenCV_CLASS();//클래스 생성
            covertfilter = new OpenCV_filter();
            detect = new OpenCV_Detection();
            openCV_Action = new OpenCV_action();

            camTimer.Enabled = false;//카메라 타이머 이벤트 초기화
            cvsInSightDisplay1.Enabled = false;
            cvsInSightDisplay1.Visible = false;

            cvsInSightDisplay1.InSight.SoftOnline = false;
            cvsInSightDisplay1.InSight.LiveAcquisition = false;

            resizeCBB.SelectedIndex = 9;//초기 크기 비율
            cropXY = new int[4];//크롭 위치 저장

            xy = new float[8];

            //회전 초기값 0도로 설정
            angleCBB.SelectedIndex = 0;

            
        }
        //파일열기
        private void openfile_Click(object sender, EventArgs e)
        {
            try
            {
                stopMotion();
                pictureBox1.Enabled = true;
                pictureBox1.Visible = true;
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    if (dlg.FileName.EndsWith(".mp4") || dlg.FileName.EndsWith(".avi"))
                    {
                        video = new VideoCapture(dlg.FileName);
                        image = new Mat();
                        videoTimer.Enabled = true;

                    }
                    else
                    {
                        image = Cv2.ImRead(dlg.FileName);
                        pictureBox1.Size = new System.Drawing.Size(image.Width, image.Height);
                        pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
                    }
                    
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void videoTimer_Tick(object sender, EventArgs e)
        {
            if (video.IsOpened())
            {
                if (video.Read(image) && !image.Empty())
                {
                    pictureBox1.Image = BitmapConverter.ToBitmap(image);
                }
                else
                {
                    stopMotion();
                }
            }
        }

        //저장하기
        private void savefileBTN_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp",
                Title = "이미지 저장",
                FileName = "default_image"  // 기본 파일 이름
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // 선택한 경로로 이미지 저장
                SaveImage(image.ToBitmap(), filePath);

                Console.WriteLine("이미지가 저장되었습니다: " + filePath);
            }
            else
            {
                Console.WriteLine("파일 저장이 취소되었습니다.");
            }
        }
        static void SaveImage(Bitmap bitmap, string filePath)
        {
            try
            {
                // Bitmap을 파일로 저장 (파일 확장자에 따라 포맷 자동 결정)
                string extension = Path.GetExtension(filePath).ToLower();
                ImageFormat format;

                switch (extension)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        format = ImageFormat.Png;
                        break;
                }

                bitmap.Save(filePath, format);
            }
            catch (Exception ex)
            {
                Console.WriteLine("이미지 저장 중 오류 발생: " + ex.Message);
            }
        }

        //대칭
        private void symmetryY_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image == null) return;
            //원본을 결과값으로 교체
            image = convert.SymmetryY(image);
            //좌우대칭 출력
            pictureBox1.Image = image.ToBitmap();

        }
        private void symmetryX_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            image = convert.SymmetryX(image);
            pictureBox1.Image = image.ToBitmap();

        }

        //피라미드형식 확대(2의 배수, 빈공간 샘플링)
        private void zoomin_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;

            image = convert.ZoomIn(image);
            pictureBox1.Image = image.ToBitmap();
        }
        //축소
        private void zoomout_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            image = convert.ZoomOut(image);
            pictureBox1.Image = image.ToBitmap();
        }
        //사이즈 조절
        private void resizeCBB_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            if (pictureBox1.Image == null) return;
            string selectItem = e.Item.Text;
            int size = Convert.ToInt32(selectItem.Replace("%", ""));
            pictureBox1.Image = convert.ResizeImage(image, size).ToBitmap();
        }
        //자르기
        //자르기 버튼 클릭시
        private void cropBTN_Click(object sender, EventArgs e)
        {
            cropSelection = true;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //평상시
            Cursor = Cursors.Default;
            try
            {
                //자르기 버튼 클릭시
                if (cropSelection|| percentColor_flag)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        Cursor = Cursors.Cross;
                        cropX = e.X;
                        cropY = e.Y;

                        cropPen = new Pen(Color.Red, 1);
                        cropPen.DashStyle = DashStyle.DashDotDot;
                    }
                    pictureBox1.Refresh();
                }
            }
            catch (Exception ex) { }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //자르기 눌렀다때기
            if (cropSelection || percentColor_flag)
            {
                Cursor = Cursors.Default;
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //평상시
            Cursor = Cursors.Default;
            try
            {
                if (cropSelection || percentColor_flag)
                {
                    if (pictureBox1 == null) return;
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        pictureBox1.Refresh();
                        cropWidth = e.X - cropX;
                        cropHeight = e.Y - cropY;
                        pictureBox1.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);
                        cropXY[0] = cropX; cropXY[1] = cropY; cropXY[2] = cropWidth; cropXY[3] = cropHeight;
                    }
                }
            }
            catch (Exception ex) { }
        }
        //자르기 실행
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (cropSelection)
                {
                    if (cropWidth < 1)
                    {
                        return;
                    }
                    image = convert.Cutting(image, cropXY[0], cropXY[1], cropXY[2], cropXY[3]);
                    pictureBox1.Image = image.ToBitmap();
                    cropSelection = false;
                }
                if (percentColor_flag)
                {
                    image = convert.Cutting(image, cropXY[0], cropXY[1], cropXY[2], cropXY[3]);
                    openCV_Action.whatIsColor(image);
                    MessageBox.Show(openCV_Action.ColorStr);//팝업
                    percentColor_flag = false;
                }
            }
            catch (Exception ex) { }
        }
        

        //카메라출력
        private void camBTN0_Click(object sender, EventArgs e)
        {
            stopMotion();
            pictureBox1.Enabled = true;
            pictureBox1.Visible = true;

            video = new VideoCapture(0);//첫카메라
            image = new Mat();
            try
            {
                camTimer.Enabled = true;
            }
            catch (Exception ex) { }
        }
        //이미지 출력시, 이전 이미지 출력 정지, 카메라 출력시 타이머 정지
        private void stopMotion()
        {
            if (video != null) video.Dispose();
            videoTimer.Enabled = false;
            if(image != null) image.Dispose();
            camTimer.Enabled = false;

            pictureBox1.Visible = false;
            pictureBox1.Enabled = false;

            IsConnected1 = false;
            cvsInSightDisplay1.Enabled = false;
            cvsInSightDisplay1.Visible = false;
        }
        private void camTimer_Tick(object sentrier, EventArgs e)
        {
            try
            {
                video.Read(image);
                if (coinBTN_flag) pictureBox1.Image = openCV_Action.cointCheck(image).ToBitmap();
                else if (deco1) pictureBox1.Image = openCV_Action.faceDecter(image).ToBitmap();
                else if(deco2) pictureBox1.Image = openCV_Action.faceDecoEar(image, earImage1).ToBitmap();
                //인수 원본, 전경 이미지 위치, 전경 가로, 전경 세로, 전경 시작좌표 X, Y, 머리 위?
                else if (deco3) pictureBox1.Image = openCV_Action.faceDeco(image, girlHairImage, 2, 3, 0.6, 0.5, true).ToBitmap();
                else if (deco4) pictureBox1.Image = openCV_Action.faceDeco(image, baldHeadImage, 1, 0.5, 0, 0.3, true).ToBitmap();
                else if (deco5) pictureBox1.Image = openCV_Action.faceDeco(image, sunglassesImage, 1, 0.3, 0, 0.2, false).ToBitmap();
                else pictureBox1.Image = image.ToBitmap();
            }
            catch (Exception ex) { }
        }


        private void cognexBTN_Click(object sender, EventArgs e)
        {
            stopMotion();
            cvsInSightDisplay1.Enabled = true;
            cvsInSightDisplay1.Visible = true;
            try
            {
                // 카메라가 연결되지 않은 상태일 때
                if (!(IsConnected1))
                {
                    cvsInSightDisplay1.Connect("172.31.6.9", "admin", "", false);
                    IsConnected1 = true;

                    cvsInSightDisplay1.ImageScale = 0.84; // 촬영중인 이미지의 배율 설정
                    cvsInSightDisplay1.ShowImage = true; // 카메라가 취득한 이미지를 보여줌
                    cvsInSightDisplay1.ShowGraphics = true;


                    Online_Check();
                }
                else // 카메라가 연결된 상태일 때
                {
                    insight.Disconnect(); // 연결된 카메라와의 접속을 끊음
                    IsConnected1 = false;
                    cvsInSightDisplay1.ShowImage = false; // 카메라가 취득한 이미지를 가림
                    cvsInSightDisplay1.ShowGraphics = false;
                }
            }
            catch { }

        }
        private void CognexLiveCB_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            if (cvsInSightDisplay1.InSight.SoftOnline)
            {
                cvsInSightDisplay1.InSight.SoftOnline = !cvsInSightDisplay1.InSight.SoftOnline;
            }
            cvsInSightDisplay1.InSight.LiveAcquisition = !cvsInSightDisplay1.InSight.LiveAcquisition;
            Online_Check();
        }
        private void Online_Check()
        {
            // OnLineST1 에 카메라의 온라인 상태 여부를 할당
            OnLineST1 = cvsInSightDisplay1.SoftOnline;

            // 카메라가 온라인일 때
            if (OnLineST1 == true)
            {
                trigerBTN.Enabled = false; //오프라인 일 때 트리거 비활성화
            }
            // 카메라가 오프라인일 때
            else if (OnLineST1 == false)
            {
                trigerBTN.Enabled = true; //오프라인 일 때 트리거 활성화
            }
        }

        private void trigerBTN_Click(object sender, EventArgs e)
        {
            if (IsConnected1)
            {
                if (cvsInSightDisplay1.InSight.LiveAcquisition)
                {
                    CognexLiveCB.Checked = false;
                    cvsInSightDisplay1.InSight.LiveAcquisition = !cvsInSightDisplay1.InSight.LiveAcquisition;
                }
                cvsInSightDisplay1.InSight.ManualAcquire(wait: true);
                pictureBox1.Enabled = true;
                pictureBox1.Visible = true;

                Bitmap bin = cvsInSightDisplay1.GetBitmap();
                image = BitmapConverter.ToMat(bin);
                pictureBox1.Image = cvsInSightDisplay1.GetBitmap();

                cvsInSightDisplay1.Enabled = false;
                cvsInSightDisplay1.Visible = false;
            }
            if (camTimer.Enabled)
            {
                pictureBox1.Visible=true;
                pictureBox1.Enabled=true;
                pictureBox1.Image = image.ToBitmap();
            }
            camTimer.Enabled = false;
            //메모리 해제
            if (cvsInSightDisplay1 != null) cvsInSightDisplay1.Dispose();
            if(video != null) video.Dispose();  
        }

        private void grayBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            image = covertfilter.GrayScale(image);
            pictureBox1.Image = image.ToBitmap();
        }

        private void binBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            image = covertfilter.Binary(image, 150);
            pictureBox1.Image = image.ToBitmap();
            UserControl_popOut();
            binBTN_flag = true;
            PopupUserControl();
            userControl_Bin.TrackBar.Value = 150;
        }

        private void cannyBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = detect.Canny(image).ToBitmap();
        }

        private void cornerBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = detect.Corner(image, 10).ToBitmap();
        }

        private void affineBTN_Click(object sender, EventArgs e)
        {
            UserControl_popOut();
            if (pictureBox1.Image == null) return;//이미지 없을 시 실행 안됌
            affine_flag = true;
            PopupUserControl();
            bin = new Mat();
            Cv2.CopyTo(image, bin);
        }

        private void tesseractBTN_Click(object sender, EventArgs e)
        {
            UserControl_popOut();
            if (pictureBox1.Image == null) return;
            tesseract_flag = true;
            PopupUserControl();
        }

        private void UserControl_DilEro_endBTNevent()
        {
            UserControl_popOut();
        }

        private void UserControl_DilEro_eroTBarevent()
        {
            if (pictureBox1.Image == null) return;
            if(de != null)de.Dispose();
            de = new Mat();
            Cv2.CopyTo(image, de);
            de = covertfilter.Ero(de, userControl_DilEro.EroTBar.Value);
            de = covertfilter.Dil(de, userControl_DilEro.DilTBar.Value);
            pictureBox1.Image = de.ToBitmap();
        }

        private void UserControl_DilEro_dilTBarevent()
        {
            if (pictureBox1.Image == null) return;
            if (de != null) de.Dispose();
            de = new Mat();
            Cv2.CopyTo(image, de);
            de = covertfilter.Ero(de, userControl_DilEro.EroTBar.Value);
            de = covertfilter.Dil(de, userControl_DilEro.DilTBar.Value);
            pictureBox1.Image = de.ToBitmap();
        }

        private void UserControl_DilEro_applyBTNevent()
        {
            if (pictureBox1.Image == null) return;
            Cv2.CopyTo(de, image);
            pictureBox1.Image = image.ToBitmap();
        }

        private void UserControl_Bin_endBTNevent()
        {
            UserControl_popOut();
        }
        private void UserControl_Bin_button1event()
        {
            if (pictureBox1.Image == null) return;
            image = covertfilter.Binary(image, userControl_Bin.TrackBar.Value);
            pictureBox1.Image = image.ToBitmap();
        }

        private void UserControl_Bin_trackBar1event()
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = covertfilter.Binary(image, userControl_Bin.TrackBar.Value).ToBitmap();
        }

        private void UserControl_Tesseract_endBTNtEvent()
        {
            this.panel2.Visible = false;
            this.Controls.Remove(userControl_Tesseract);
            userControl_Tesseract.Dispose();
            tesseract_flag= false;  
        }

        private void UserControl_Tesseract_TesseractEvent()
        {
            userControl_Tesseract.ResultLB.Text = detect.Tesseract(image);
        }
        private void dileroBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            UserControl_popOut();
            dileroBTN_flag = true;
            PopupUserControl();
            de = new Mat();
            Cv2.CopyTo(image, de);
        }

        private void gradientBTN_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image == null) return;
            pictureBox1.Image = covertfilter.Gradient(image).ToBitmap();
        }

        private void tophatBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = covertfilter.Tophat(image).ToBitmap();
        }

        private void blackhatBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = covertfilter.Blackhat(image).ToBitmap();
        }
 
       //아핀변환
        private void UserControl_affine1_returnBTN_event()
        {
            if (pictureBox1.Image == null) return;
            pictureBox1.Image = image.ToBitmap();
        }

        private void UserControl_affine1_applyBTN_event()
        {
            if (pictureBox1.Image == null) return;
            image = detect.Affine(image, xy);
            pictureBox1.Image = image.ToBitmap();
        }

        private void UserControl_affine1_confirmBTN_event()
        {
            pictureBox1.Image = detect.Affine(image, xy).ToBitmap();
        }
        //아핀변환에 사용 in popup유저컨트롤
        private void UserControl_affine1_endBTN_event()
        {
            UserControl_popOut();
        }
        //아핀변환에 사용
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null) return;
            if (affine_flag)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (clickCount > 3)
                    {
                        clickCount = 0;
                        bin.Dispose();
                        bin = new Mat();
                        Cv2.CopyTo(image, bin);
                        userControl_affine1.LuxTB.Text = "";
                        userControl_affine1.LuyTB.Text = "";
                        userControl_affine1.LdxTB.Text = "";
                        userControl_affine1.LdyTB.Text = "";
                        userControl_affine1.RuxTB.Text = "";
                        userControl_affine1.RuyTB.Text = "";
                        userControl_affine1.RdxTB.Text = "";
                        userControl_affine1.RdyTB.Text = "";
                    }
                    Cursor = Cursors.Cross;
                    switch (clickCount)
                    {
                        case 0:
                            lux = e.X;
                            luy = e.Y;
                            userControl_affine1.LuxTB.Text = lux.ToString();
                            userControl_affine1.LuyTB.Text = luy.ToString();
                            Cv2.Circle(bin, (int)lux, (int)luy, 5, Scalar.Red, 2, LineTypes.AntiAlias);
                            pictureBox1.Image = bin.ToBitmap();
                            clickCount++;
                            break;
                        case 1:
                            ldx = e.X;
                            ldy = e.Y;
                            userControl_affine1.LdxTB.Text = ldx.ToString();
                            userControl_affine1.LdyTB.Text = ldy.ToString();
                            Cv2.Circle(bin, (int)ldx, (int)ldy, 5, Scalar.Blue, 2, LineTypes.AntiAlias);
                            pictureBox1.Image = bin.ToBitmap();
                            clickCount++;
                            break;
                        case 2:
                            rux = e.X;
                            ruy = e.Y;
                            userControl_affine1.RuxTB.Text = rux.ToString();
                            userControl_affine1.RuyTB.Text = ruy.ToString();
                            Cv2.Circle(bin, (int)rux, (int)ruy, 5, Scalar.Yellow, 2, LineTypes.AntiAlias);
                            pictureBox1.Image = bin.ToBitmap();
                            clickCount++;
                            break;
                        case 3:
                            rdx = e.X;
                            rdy = e.Y;
                            userControl_affine1.RdxTB.Text = rdx.ToString();
                            userControl_affine1.RdyTB.Text = rdy.ToString();
                            Cv2.Circle(bin, (int)rdx, (int)rdy, 5, Scalar.Green, 2, LineTypes.AntiAlias);
                            pictureBox1.Image = bin.ToBitmap();
                            inputText();
                            clickCount++;
                            break;
                        default:
                            clickCount = 0;
                            MessageBox.Show("영역을 다시 설정하세요.");
                            break;
                    }
                }
                pictureBox1.Refresh();
            }
        }
        private void inputText()
        {
            double x = Convert.ToDouble(lux);
            xy[0] = (float)x;
            x = Convert.ToDouble(luy);
            xy[1] = (float)x;
            x = Convert.ToDouble(ldx);
            xy[2] = (float)x;
            x = Convert.ToDouble(ldy);
            xy[3] = (float)x;
            x = Convert.ToDouble(rux);
            xy[4] = (float)x;
            x = Convert.ToDouble(ruy);
            xy[5] = (float)x;
            x = Convert.ToDouble(rdx);
            xy[6] = (float)x;
            x = Convert.ToDouble(rdy);
            xy[7] = (float)x;
        }

        private void circleBTN_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = detect.Circle(image).ToBitmap();
        }
        //블러
        private void blurBTN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            UserControl_popOut();
            blur_flag = true;
            PopupUserControl();
        }
        private void UserControl_Blur_applyBTNEvent()
        {
            UserControl_Blur_blurTBarEvent();
            Cv2.CopyTo(applyBlur, image);
        }

        private void UserControl_Blur_endBTNEvent()
        {
            UserControl_popOut();
        }

        private void UserControl_Blur_blurTBarEvent()
        {
            if(pictureBox1.Image == null) return ;
            if (userControl_Blur.BlurRB.Checked)
            {
                if(userControl_Blur.Blur_trackBar.Value==0) return;//0이면 실행 안함
                int kernel = oddNumber_Check(userControl_Blur.Blur_trackBar.Value);//커널 홀수로 만들어줌
                applyBlur = covertfilter.Blur(image, kernel);
                pictureBox1.Image = applyBlur.ToBitmap();


            }
            if (userControl_Blur.GaussianRB.Checked)
            {
                if (userControl_Blur.Gaussian_trackBar.Value == 0) return;
                int kernel = oddNumber_Check(userControl_Blur.Gaussian_trackBar.Value);
                applyBlur = covertfilter.Gaussian(image, kernel);
                pictureBox1.Image = applyBlur.ToBitmap();
            }
        }
        public int oddNumber_Check(int num)
        {
            if (num % 2 == 0) num++;
            return num;
        }

        private void cardBTN_Click(object sender, EventArgs e)
        {
            if (image == null) return ;
            pictureBox1.Image = openCV_Action.CardDetect(image).ToBitmap();
            MessageBox.Show(openCV_Action.Text);
        }

        private void allColorBTN_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = openCV_Action.AllCircleColor(image).ToBitmap();
        }

        private void angleCBB_DropDownItemClicked(object sender, RibbonItemEventArgs e)
        {
            System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
            string angleCBB_text = button.Text.Replace("°", "");
            image = convert.Rotation(image, int.Parse(angleCBB_text));
            pictureBox1.Image = image.ToBitmap();
        }

        private void coinBTN_Click(object sender, EventArgs e)
        {
            coinBTN_flag = !coinBTN_flag;
        }

        private void squareBTN_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = detect.Square(image).ToBitmap();
        }

        private void barcodeBTN_Click(object sender, EventArgs e)
        {
            UserControl_popOut();
            barcode_flag = true;
            PopupUserControl();
        }

        //색상검출
        private void hsvBTN_Click(object sender, EventArgs e)
        {
            UserControl_popOut();
            color_flag =true;
            PopupUserControl();
        }

        private void UserControl_Color_endBtnEvent()
        {
            UserControl_popOut();
        }

        private void UserControl_Color_applyBtnEvent()
        {
            string colorText = userControl_Color.ColorText;
            image = detect.Color(image, colorText);
            pictureBox1.Image = image.ToBitmap();
        }

        private void UserControl_Color_colorBtnEvent()
        {
            string colorText= userControl_Color.ColorText;
            pictureBox1.Image = detect.Color(image, colorText).ToBitmap();
        }

        

        //부분 색상 검출
        private void percentBTN_Click(object sender, EventArgs e)
        {
            percentColor_flag = true;
        }

        //바코드 
        private void UserControl_Barcode_event_endBTN_Click()
        {
            UserControl_popOut();
        }
        private void UserControl_Barcode_event_UC_select_Click()
        {
            string name = userControl_Barcode.TextBox1.Text;
            db.select(name);
            userControl_Barcode.DataGridView1.DataSource = db.dt;
        }
        private void UserControl_Barcode_event_UC_barcodeBTN_Click()
        {
            userControl_Barcode.PictureBox1.Image = pictureBox1.Image;
            string decoded = "";
            BarcodeReader reader = new BarcodeReader();//인스턴스 생성
            Result result = reader.Decode((Bitmap)userControl_Barcode.PictureBox1.Image);//바코드 해석
            db.connect();
            // 결과를 확인합니다.
            if (result != null)
            {
                int[] num = { 2, 4, 2, 4, 1 };
                int currentPosition = 0;

                for (int i = 0; i < num.Length; i++)
                {
                    if (currentPosition + num[i] <= result.Text.Length)
                    {
                        string segment = result.Text.Substring(currentPosition, num[i]);
                        currentPosition += num[i];

                        if (i == 0)
                        {
                            switch (segment)
                            {
                                case "11":
                                    company = "삼성";
                                    break;
                                case "21":
                                    company = "엘지";
                                    break;
                                case "88":
                                    company = "씨제이";
                                    break;
                                case "99":
                                    company = "오뚜기";
                                    break;
                            }
                        }
                        else if (i == 1)
                        {
                            switch (segment)
                            {
                                case "1234":
                                    product = "세탁기";
                                    break;
                                case "1245":
                                    product = "냉장고";
                                    break;
                                case "1256":
                                    product = "정수기";
                                    break;
                                case "1267":
                                    product = "에어컨";
                                    break;
                                case "3412":
                                    product = "햇반";
                                    break;
                                case "3413":
                                    product = "밀가루";
                                    break;
                                case "3415":
                                    product = "만두";
                                    break;
                                case "3416":
                                    product = "라면";
                                    break;
                            }
                        }
                        else if (i == 2)
                        {
                            switch (segment)
                            {
                                case "11":
                                    production = "한국";
                                    break;
                                case "12":
                                    production = "차이나";
                                    break;
                                case "13":
                                    production = "베트남";
                                    break;
                                case "14":
                                    production = "태국";
                                    break;
                            }
                        }
                        else if (i == 3)
                        {
                            string yearSub = segment.Substring(0, 2);
                            string monthSub = segment.Substring(2, 2);
                            year = 2000 + int.Parse(yearSub);
                            month = int.Parse(monthSub);

                            date = new DateTime(year, month, 1);
                        }
                        else if (i == 4)
                        {
                            string rt = result.Text; // EAN-13 바코드 문자열
                            int len = rt.Length;

                            if (len != 13)
                            {
                                throw new ArgumentException("EAN-13 바코드는 13자리 숫자로 구성되어야 합니다.");
                            }

                            int sumOdd = 0; // 홀수 자리 숫자의 합
                            int sumEven = 0; // 짝수 자리 숫자의 합

                            // 홀수 및 짝수 자리에 있는 숫자 합산 (1-based index 기준)
                            for (int j = 0; j < len - 1; j++)
                            {
                                int digit = int.Parse(rt[j].ToString());

                                if (j % 2 == 0) // 0-based index에서 홀수 자리에 해당 (1, 3, 5, ...)
                                {
                                    sumOdd += digit;
                                }
                                else
                                {
                                    sumEven += digit;
                                }
                            }

                            // 홀수 자리 합에 3을 곱하고, 짝수 자리 합과 더함
                            int total = sumOdd + (sumEven * 3);

                            // 총합을 10으로 나눈 나머지를 구하고, 체크디지트 계산
                            int remainder = total % 10;
                            int checkDigit = (10 - remainder) % 10;

                            // 실제 체크디지트 (마지막 자리 숫자) 추출
                            int actualCheckDigit = int.Parse(rt.Substring(len - 1, 1));

                            // 체크디지트 검증
                            luhn = (checkDigit == actualCheckDigit);
                        }
                        else
                        {
                            test = segment;
                            // 바코드를 인식하지 못한 경우의 처리
                        }
                    }
                }
                //해석된 텍스트, 바코드 형식
                decoded = result.ToString() + "\r\n" + result.BarcodeFormat.ToString()
                     + $"\r\n {company}\r\n {product}\r\n {production}\r\n {date} \r\n {luhn}";
                userControl_Barcode.Label1.Text = decoded;
                if (luhn) db.insert(company, product, production, date);
                MessageBox.Show($"바코드 오류 :{sum}");
                userControl_Barcode.DataGridView1.DataSource = db.dt;
            }
            else
            {
                userControl_Barcode.Label1.Text = "바코드를 인식하지 못하였습니다.";
            }
        }

        private void UserControl_Barcode_event_UC_camBTN_Click()
        {
            camTimer.Enabled = !camTimer.Enabled;
            pictureBox1.Enabled = camTimer.Enabled;
            pictureBox1.Visible = camTimer.Enabled;
            if (camTimer.Enabled)
            {
                video = new VideoCapture(0);//첫카메라
                image = new Mat();
            }
        }


        //얼굴검출
        private void faceBTN_Click(object sender, EventArgs e)
        {
            UserControl_popOut();
            face_flag = true;
            PopupUserControl();
        }
        private void UserControl_Face_event_decorationBTN1()
        {
            deco1 = !deco1;
            decoFlag();
        }
        private void UserControl_Face_event_decorationBTN2()
        {
            deco2 = !deco2;
            decoFlag();
        }
        private void UserControl_Face_event_girlHairBTN()
        {
            deco3 = !deco3;
            decoFlag();
        }
        private void UserControl_Face_event_baldHeadBTN()
        {
            deco4 = !deco4;
            decoFlag();
        }

        private void UserControl_Face_event_sunglassesBTN()
        {
            deco5 = !deco5;
            decoFlag();
        }
        private void decoFlag()
        {
            if (deco1)
            {
                deco2 = false;
                deco3 = false;
                deco4 = false;
                deco5 = false;
            }
            if(deco2)
            {
                deco1 = false;
                deco3 = false;
                deco4 = false;
                deco5 = false;
            }
            if (deco3)
            {
                deco1 = false;
                deco2 = false;
                deco4 = false;
                deco5 = false;
            }
            if (deco4)
            {
                deco1 = false;
                deco2 = false;
                deco3 = false;
                deco5 = false;
            }
            if (deco5)
            {
                deco1 = false;
                deco2 = false;
                deco3 = false;
                deco4 = false;
            }
        }
        
        private void UserControl_Face_event_endBTN()
        {
            UserControl_popOut();
        }
        private void UserControl_Face_event_camBTN()
        {
            camTimer.Enabled = !camTimer.Enabled;
            pictureBox1.Enabled = camTimer.Enabled;
            pictureBox1.Visible = camTimer.Enabled;
            if (camTimer.Enabled)
            {
                video = new VideoCapture(0);//첫카메라
                image = new Mat();
            }
        }

        
        //유저컨트롤 생성 및 삭제
        private void PopupUserControl()
        {
            this.panel2.Visible = true;
            if (affine_flag)
            {
                userControl_affine1 = new UserControl_affine();
                this.panel2.Controls.Add(userControl_affine1);

                userControl_affine1.confirmBTN_event += UserControl_affine1_confirmBTN_event;
                userControl_affine1.applyBTN_event += UserControl_affine1_applyBTN_event;
                userControl_affine1.returnBTN_event += UserControl_affine1_returnBTN_event;
                userControl_affine1.endBTN_event += UserControl_affine1_endBTN_event;
            }
            if (tesseract_flag)
            {
                userControl_Tesseract = new UserControl_Tesseract();
                this.panel2.Controls.Add(userControl_Tesseract);

                userControl_Tesseract.TesseractEvent += UserControl_Tesseract_TesseractEvent;
                userControl_Tesseract.endBTNtEvent += UserControl_Tesseract_endBTNtEvent;
            }
            if (binBTN_flag)
            {
                userControl_Bin = new UserControl_Bin();
                this.panel2.Controls.Add(userControl_Bin);

                userControl_Bin.trackBar1event += UserControl_Bin_trackBar1event;
                userControl_Bin.button1event += UserControl_Bin_button1event;
                userControl_Bin.endBTNevent += UserControl_Bin_endBTNevent;
            }
            if (dileroBTN_flag)
            {
                userControl_DilEro = new UserControl_DilEro();
                this.panel2.Controls.Add(userControl_DilEro);

                userControl_DilEro.applyBTNevent += UserControl_DilEro_applyBTNevent;
                userControl_DilEro.dilTBarevent += UserControl_DilEro_dilTBarevent;
                userControl_DilEro.eroTBarevent += UserControl_DilEro_eroTBarevent;
                userControl_DilEro.endBTNevent += UserControl_DilEro_endBTNevent;
            }
            if (blur_flag)
            {
                userControl_Blur = new UserControl_blur();
                this.panel2.Controls.Add(userControl_Blur);

                userControl_Blur.blurTBarEvent += UserControl_Blur_blurTBarEvent;
                userControl_Blur.endBTNEvent += UserControl_Blur_endBTNEvent;
                userControl_Blur.applyBTNEvent += UserControl_Blur_applyBTNEvent;
            }
            if (color_flag)
            {
                userControl_Color = new UserControl_color();
                this.panel2.Controls.Add(userControl_Color);

                userControl_Color.colorBtnEvent += UserControl_Color_colorBtnEvent;
                userControl_Color.applyBtnEvent += UserControl_Color_applyBtnEvent;
                userControl_Color.endBtnEvent += UserControl_Color_endBtnEvent;
            }
            if (barcode_flag)
            {
                userControl_Barcode = new userControl_barcode();
                this.panel2.Controls.Add(userControl_Barcode);

                userControl_Barcode.event_UC_camBTN_Click += UserControl_Barcode_event_UC_camBTN_Click;
                userControl_Barcode.event_UC_barcodeBTN_Click += UserControl_Barcode_event_UC_barcodeBTN_Click;
                userControl_Barcode.event_UC_select_Click += UserControl_Barcode_event_UC_select_Click;
                userControl_Barcode.event_endBTN_Click += UserControl_Barcode_event_endBTN_Click;
            }
            if (face_flag)
            {
                userControl_Face = new UserControl_face();
                this.panel2.Controls.Add(userControl_Face);

                userControl_Face.event_endBTN += UserControl_Face_event_endBTN;
                userControl_Face.event_camBTN += UserControl_Face_event_camBTN;
                userControl_Face.event_decorationBTN1 += UserControl_Face_event_decorationBTN1;
                userControl_Face.event_decorationBTN2 += UserControl_Face_event_decorationBTN2;
                userControl_Face.event_girlHairBTN += UserControl_Face_event_girlHairBTN;
                userControl_Face.event_baldHeadBTN += UserControl_Face_event_baldHeadBTN;
                userControl_Face.event_sunglassesBTN += UserControl_Face_event_sunglassesBTN;
            }
        }


        private void UserControl_popOut()
        {
            if (userControl_affine1 != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_affine1);
                userControl_affine1.Dispose();
                affine_flag = false;
            }
            if (userControl_Tesseract != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_Tesseract);
                userControl_Tesseract.Dispose();
                tesseract_flag = false;
            }
            if (userControl_Bin != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_Bin);
                userControl_Bin.Dispose();
                binBTN_flag = false;
            }
            if (userControl_DilEro != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_DilEro);
                userControl_DilEro.Dispose();
                dileroBTN_flag = false;
            }
            if (userControl_Blur != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_Blur);
                userControl_Blur.Dispose();
                blur_flag = false;
            }
            if (userControl_Color != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_Color);
                userControl_Color.Dispose();
                color_flag = false;
            }
            if (userControl_Barcode != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_Barcode);
                userControl_Barcode.Dispose();
                barcode_flag = false;
            }
            if (userControl_Face != null)
            {
                this.panel2.Visible = false;
                this.Controls.Remove(userControl_Face);
                userControl_Face.Dispose();
                face_flag = false;
            }
        }
    }
}
