using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XPhoto;
using Tesseract;
using static System.Net.Mime.MediaTypeNames;
using static ZXing.QrCode.Internal.Mode;

namespace openCV0820
{
    internal class OpenCV_action : IDisposable
    {
        OpenCV_filter openCV_Filter = new OpenCV_filter();
        OpenCV_Detection openCV_Detection = new OpenCV_Detection();
        Mat card;
        Mat affine;
        Mat circle;
        Mat coin;
        String text;//결과 글자
        string colorStr;//색상 검출 백분율
        string biggestColor;
        string maxColor;
        string coinSize;
        //얼굴 검출
        Mat haarface;
        string filePath;
        CascadeClassifier faceCascade;
        //얼굴 데코
        Mat decoration;
        Mat decoImage;
        public OpenCV_action()
        {
            filePath = "../../bin/Debug/haarcascade_frontalface_alt/haarcascade_frontalface_alt.xml";
        }
        public string ColorStr
        {
            get { return colorStr; }
            set { colorStr = value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public Mat CardDetect(Mat src)
        {
            card = new Mat();
            OpenCvSharp.Point[] square = openCV_Detection.FindSquare(src);
            List<Point2f> src_pts = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, src.Height),
                new Point2f(src.Width, 0.0f),
                new Point2f(src.Width, src.Height)
            };

            square = sortPoint(square);
            List<Point2f> affine_pts = new List<Point2f>()
            {
                new Point2f(square[0].X, square[0].Y),
                new Point2f(square[1].X, square[1].Y),
                new Point2f(square[2].X, square[2].Y),
                new Point2f(square[3].X, square[3].Y)
            };
            Mat matrix = Cv2.GetPerspectiveTransform(affine_pts, src_pts);
            Cv2.WarpPerspective(src, card, matrix, new OpenCvSharp.Size(src.Width, src.Height));

            Text = openCV_Detection.Tesseract(card);

            return card;
        }

        private OpenCvSharp.Point[] sortPoint(OpenCvSharp.Point[] square)
        {
            OpenCvSharp.Point[] sort = new OpenCvSharp.Point[4];
            int min1 = int.MaxValue;
            int min2 = int.MaxValue;
            int minX1 = 0;
            int minX2 = 0;
            for(int i =0; i < 4; i++)
            {
                if (square[i].X < min1)
                {
                    min1 = square[i].X;
                    minX1 = i;
                }
                else if (square[i].X <= min2 && i != minX1)
                {
                    min2 = square[i].X;
                    minX2 = i;
                }
                
            }
            //왼쪽 좌표 위아래 정렬
            if (square[minX1].Y < square[minX2].Y)
            {
                sort[0] = square[minX1];
                sort[1] = square[minX2];
            }
            else
            {
                sort[1] = square[minX1];
                sort[0] = square[minX2];
            }

            //안뽑힌 두수 뽑기
            int[] maxX = new int[2];
            int count = 0;
            for(int i =0; i < 4; i++)
            {
                if (i != minX1 && i != minX2 && count ==0)
                {
                    maxX[0] = i;
                    count++;
                }
                else if(i != minX1 && i != minX2 && maxX[0] != i)
                {
                    maxX[1] = i;
                }
            }
            //오른쪽 위아래 정렬
            if(square[maxX[0]].Y < square[maxX[1]].Y)
            {
                sort[2] = square[maxX[0]];
                sort[3] = square[maxX[1]];
            }
            else
            {
                sort[2] = square[maxX[1]];
                sort[3] = square[maxX[0]];
            }
            return sort;
        }

        public void whatIsColor(Mat src)
        {
            //난수설정
            Random wRandom = new Random();
            Random hRandom = new Random();
            Mat hsvImage = new Mat();
            Cv2.CvtColor(src, hsvImage, ColorConversionCodes.BGR2HSV);
            //랜덤 지점 설정
            int wNum;
            int hNum;
            //검출된 색상 정보
            Vec3b hsvColor;
            byte hue;//색상
            byte saturation;//채도
            byte value;//명도
            //색상 카운트
            int redCount = 0;
            int orangeCount = 0;
            int yellowCount = 0;
            int greenCount = 0;
            int cyanCount = 0;
            int blueCount = 0;
            int magentaCount = 0;
            int pinkCount = 0;
            int allCount = 0;
            //무채색 카운트
            int blackCount = 0;
            int whiteCount = 0;
            //창에 쓸 문구
            colorStr = "";
            //난수 발생
            while (allCount<100)
            {
                wNum = wRandom.Next(hsvImage.Cols);
                hNum = hRandom.Next(hsvImage.Rows);
                hsvColor = hsvImage.At<Vec3b>(hNum, wNum);//색상검출
                hue = hsvColor.Item0;//색상
                saturation = hsvColor.Item1;//채도
                value = hsvColor.Item2;//명도

                // 채도와 명도 범위 (0~255)
                int minSaturation = 50;
                int maxSaturation = 255;
                int minValue = 50;
                int maxValue = 255;

                // 흑색과 백색 정의
                int minBlackValue = 0;
                int maxBlackValue = 50;
                int minWhiteValue = 200;
                int maxWhiteValue = 255;
                int maxBlackSaturation = 50; // 흑색은 채도가 매우 낮음
                int maxWhiteSaturation = 30;  // 백색은 채도가 낮음
                if (saturation >= minSaturation && saturation <= maxSaturation &&
                    value >= minValue && value <= maxValue)
                {
                    if ((hue >= 0 && hue <= 10) || (hue >= 170 && hue <= 180))
                        redCount++;
                    else if (hue >= 11 && hue <= 25)
                        orangeCount++;
                    else if (hue >= 26 && hue <= 35)
                        yellowCount++;
                    else if (hue >= 36 && hue <= 70)
                        greenCount++;
                    else if (hue >= 71 && hue <= 100)
                        cyanCount++;
                    else if (hue >= 101 && hue <= 130)
                        blueCount++;
                    else if (hue >= 131 && hue <= 160)
                        magentaCount++;
                    else if (hue >= 161 && hue <= 170)
                        pinkCount++;
                }
                else if (saturation <= maxBlackSaturation && value >= minBlackValue && value <= maxBlackValue)
                {
                    blackCount++;
                }
                else if (saturation <= maxWhiteSaturation && value >= minWhiteValue && value <= maxWhiteValue)
                {
                    whiteCount++;
                }
                //검흰 빼고 색상 100개 검출 완료시 끝
                allCount = redCount + orangeCount + yellowCount + greenCount
                    + cyanCount + blueCount + magentaCount + pinkCount+ blackCount+ whiteCount;
            }
            //카운트한 색상 팝업
            if (redCount != 0) colorStr = "Red : " + redCount + "%" + "\n\r";
            if (orangeCount != 0) colorStr = colorStr + "Orange : " + orangeCount + "%" + "\n\r";
            if (yellowCount != 0) colorStr = colorStr + "Yellow : " + yellowCount + "%" + "\n\r";
            if (greenCount != 0) colorStr = colorStr + "Green : " + greenCount + "%" + "\n\r";
            if (cyanCount != 0) colorStr = colorStr + "Cyan : " + cyanCount + "%" + "\n\r";
            if (blueCount != 0) colorStr = colorStr + "Blue : " + blueCount + "%" + "\n\r";
            if (magentaCount != 0) colorStr = colorStr + "Magenta : " + magentaCount + "%" + "\n\r";
            if (pinkCount != 0) colorStr = colorStr + "Pink : " + pinkCount + "%" + "\n\r";
            if (blackCount != 0) colorStr = colorStr + "Black : " + blackCount + "%" + "\n\r";
            if (whiteCount != 0) colorStr = colorStr + "White" + whiteCount + "%";

            //가장 비율이 높은 색상
            int[] counts = { redCount, orangeCount, yellowCount, greenCount, cyanCount, blueCount, magentaCount, pinkCount, blackCount, whiteCount };
            string[] colorsr = { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Magenta", "Pink", "Black", "White"};
            // 색상 및 카운트 정렬
            SortColorsByCount(ref counts, ref colorsr);

            //컬러 끝 str에 지정
            int maxCount = counts[0];
            maxColor = colorsr[0];
            biggestColor = maxColor +" : "+ maxCount.ToString()+"%";
        }
        //가장 비율이 높은 색상 구하기 
        public static void SortColorsByCount(ref int[] counts, ref string[] colors)
        {
            //색상 배열 동기화
            int length = counts.Length;
            var indices = Enumerable.Range(0, length).ToArray();

            Array.Sort(counts, indices, Comparer<int>.Create((a, b) => b.CompareTo(a)));

            string[] sortedColors = new string[length];
            for (int i = 0; i < length; i++)
            {
                sortedColors[i] = colors[indices[i]];
            }
            colors = sortedColors;
        }
        public Scalar biggestColorScalar(string str)
        {
            switch (str)
            {
                case "Red": 
                    return new Scalar(0, 255, 255);
                case "Orange":
                    return new Scalar(15, 255, 255);
                case "Yellow":
                    return new Scalar(30, 255, 255);
                case "Green":
                    return new Scalar(60, 255, 128);
                case "Cyan":
                    return new Scalar(90, 255, 128);
                case "Blue":
                    return new Scalar(120, 255, 255);
                case "Magenta":
                    return new Scalar(150, 200, 255);
                case "Pink":
                    return new Scalar(150, 128, 255);
                case "Black":
                    return new Scalar(25, 25, 25);
                case "White":
                    return new Scalar(240, 240, 240);
                default:
                    return new Scalar(0, 0, 0);
            }
        }
        public Mat AllCircleColor(Mat src)
        {
            circle = new Mat();
            Mat dst = new Mat();
            Cv2.CopyTo(src, dst);

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            if (src.Channels() != 1) Cv2.CvtColor(src, circle, ColorConversionCodes.BGR2GRAY);
            else Cv2.CopyTo(src, circle);
            //블러 추가
            Cv2.GaussianBlur(circle, circle, new OpenCvSharp.Size(11, 11), 0, 0, BorderTypes.Default);

            CircleSegment[] circles = Cv2.HoughCircles(circle, HoughModes.Gradient, 1, 100, 100, 35, 0, 0);
            for (int i = 0; i < circles.Length; i++)
            {
                OpenCvSharp.Point center = new OpenCvSharp.Point(circles[i].Center.X- circles[i].Radius, circles[i].Center.Y- circles[i].Radius);
                Mat area = new Mat(src, new OpenCvSharp.Rect((int)(circles[i].Center.X - circles[i].Radius / 2),
                                                    (int)(circles[i].Center.Y - circles[i].Radius / 2),
                                                    (int)(circles[i].Radius),
                                                    (int)(circles[i].Radius)
                                                    )
                                   );
                whatIsColor(area);
                //글자 표시하기
                Cv2.PutText(dst, biggestColor, center, HersheyFonts.HersheySimplex, 0.7, biggestColorScalar(maxColor));
            }
            return dst;
        }
        public Mat cointCheck(Mat src)
        {
            Mat dst = new Mat();
            coin = new Mat();
            Cv2.CopyTo(src, dst);
            //그레이스케일
            if (src.Channels() != 1) Cv2.CvtColor(src, coin, ColorConversionCodes.BGR2GRAY);
            else Cv2.CopyTo(src, coin);
            //블러
            Cv2.GaussianBlur(coin, coin, new OpenCvSharp.Size(11, 11), 0, 0, BorderTypes.Default);
            //원검출
            CircleSegment[] circles = Cv2.HoughCircles(coin, HoughModes.Gradient, 1, 100, 100, 35, 0, 0);
            for (int i = 0; i < circles.Length; i++)
            {
                OpenCvSharp.Point center = new OpenCvSharp.Point(circles[i].Center.X - circles[i].Radius, circles[i].Center.Y - circles[i].Radius);
                Cv2.Rectangle(dst, new OpenCvSharp.Point(circles[i].Center.X - circles[i].Radius, circles[i].Center.Y - circles[i].Radius),
                                   new OpenCvSharp.Point(circles[i].Center.X + circles[i].Radius, circles[i].Center.Y + circles[i].Radius),
                                   Scalar.Red, 1);

                coinClassification(circles[i].Radius);
                Cv2.PutText(dst, coinSize, center, HersheyFonts.HersheySimplex, 0.7, Scalar.Red);
            }

            return dst;
        }

        private void coinClassification(float rad)
        {
            //높이 약15cm일떄의 동전 크기 - 상황마다 조절 필요
            if (rad > 45 && rad <60) coinSize = "100";
            else if (rad < 80) coinSize = "500";
            else coinSize = "길이 : " + rad.ToString();
        }

        public Mat faceDecter(Mat src)
        {
            //고치기
            using (haarface = new Mat())
            {
                Cv2.CopyTo(src, haarface);
                if (haarface.Channels() != 1) Cv2.CvtColor(haarface, haarface, ColorConversionCodes.RGB2GRAY);
                Cv2.EqualizeHist(haarface, haarface);
                using (faceCascade = new CascadeClassifier(filePath))
                {
                    //FileStorage Storage = new FileStorage();
                    OpenCvSharp.Rect[] faces = faceCascade.DetectMultiScale(haarface, scaleFactor: 1.139, minNeighbors: 3);
                    foreach (var face in faces)
                    {
                        Cv2.Rectangle(src, face, new Scalar(0, 0, 255), 2);
                    }
                }  
            }
            return src;
        }
        //원본
        public Mat faceDecoEar(Mat src, string path)
        {
            decoImage = Cv2.ImRead(path, ImreadModes.Unchanged);
            // src 이미지를 복사하여 decoration을 만듭니다.
            Mat decoration = src.Clone();

            // 이미지를 회색조로 변환
            if (decoration.Channels() != 1)
            {
                Cv2.CvtColor(decoration, decoration, ColorConversionCodes.BGR2GRAY);
            }
            // 히스토그램 평활화
            Cv2.EqualizeHist(decoration, decoration);

            // 얼굴 탐지기 로드
            using (var faceCascade = new CascadeClassifier(filePath))
            {
                OpenCvSharp.Rect[] faces = faceCascade.DetectMultiScale(decoration, scaleFactor: 1.139, minNeighbors: 2);

                foreach (var face in faces)
                {
                    // 얼굴에 맞게 토끼귀 이미지 리사이즈
                    Mat resizedEar = new Mat();
                    Cv2.Resize(decoImage, resizedEar, new  OpenCvSharp.Size(face.Width, (int)(face.Height)));//귀 길이

                    // 토끼귀 위치 조정
                    int earX1 = face.X;//왼
                    int earY1 = face.Y - resizedEar.Rows- (int)(face.Height * 0.05); // 귀 위치 조정 : 상
                    int earX2 = earX1 + resizedEar.Cols;//오
                    int earY2 = earY1 + resizedEar.Rows;//하

                    // 이미지 경계 조정
                    earX1 = Math.Max(0, earX1);//0보다 작을경우 0
                    earY1 = Math.Max(0, earY1);
                    earX2 = Math.Min(src.Cols, earX2);//src보다 클경우 earX2
                    earY2 = Math.Min(src.Rows, earY2);

                    // 토끼귀 이미지 합성
                    for (int y = earY1; y < earY2; y++)
                    {
                        for (int x = earX1; x < earX2; x++)
                        {
                            if (y - earY1 < resizedEar.Rows && x - earX1 < resizedEar.Cols) // 인덱스 범위 체크
                            {
                                //Vec4b earPixel: resizedEar 이미지에서 현재 픽셀의 RGBA 값
                                //Vec4b는 4개의 바이트(각각 Red, Green, Blue, Alpha)를 포함
                                Vec4b earPixel = resizedEar.At<Vec4b>(y - earY1, x - earX1);//위치에 있는 픽셀의 값을 가져옴
                                if (earPixel[3] > 0) // 알파 값이 0이 아닌 픽셀만 처리
                                {
                                    Vec3b imgPixel = src.At<Vec3b>(y, x);

                                    // 알파 블렌딩
                                    // 두 개 이상의 이미지를 결합할 때 사용하는 기술로, 각 이미지의 투명도(알파 값)를 고려하여 합성하는 과정
                                    //이미지의 각 픽셀에서 색상과 투명도를 조절하여 최종 이미지를 생성
                                    //수식 : 출력 색상=(배경 색상×(255−알파)+전경 색상×알파)/255
                                    //*전경 : 합성할 이미지
                                    src.At<Vec3b>(y, x) = new Vec3b(
                                        (byte)((imgPixel.Item0 * (255 - earPixel[3]) + earPixel[0] * earPixel[3]) / 255),
                                        (byte)((imgPixel.Item1 * (255 - earPixel[3]) + earPixel[1] * earPixel[3]) / 255),
                                        (byte)((imgPixel.Item2 * (255 - earPixel[3]) + earPixel[2] * earPixel[3]) / 255)
                                    );
                                }
                            }
                        }
                    }
                    if (resizedEar != null) resizedEar.Dispose();
                }
            }
            return src;
        }
        //코드 줄이기
        public Mat faceDecoPreprocessing(Mat src, string path)
        {
            decoImage = Cv2.ImRead(path, ImreadModes.Unchanged);
            // src 이미지를 복사하여 decoration을 만듭니다.
            Mat decoration = src.Clone();

            // 이미지를 회색조로 변환
            if (decoration.Channels() != 1)
            {
                Cv2.CvtColor(decoration, decoration, ColorConversionCodes.BGR2GRAY);
            }
            // 히스토그램 평활화
            Cv2.EqualizeHist(decoration, decoration);

            return decoration;
        }
        public void AlphaBlending(Mat src, OpenCvSharp.Rect face, double faceWidth, double faceHeight, double FX, double FY, bool up)
        {
            //머리
            Mat resizedEar = new Mat();
            Cv2.Resize(decoImage, resizedEar, new OpenCvSharp.Size((int)face.Width * faceWidth, (int)face.Height * faceHeight));//머리길이

            //위치 조정
            int earX1 = face.X - (int)(face.Width * FX);//왼
            int earY1;
            if (up) earY1 = face.Y - (int)(face.Height * FY);
            else earY1 = face.Y + (int)(face.Height * FY);
            int earX2 = earX1 + resizedEar.Cols;//오
            int earY2 = earY1 + resizedEar.Rows;//하

            // 이미지 경계 조정
            earX1 = Math.Max(0, earX1);//0보다 작을경우 0
            earY1 = Math.Max(0, earY1);
            earX2 = Math.Min(src.Cols, earX2);//src보다 클경우 earX2
            earY2 = Math.Min(src.Rows, earY2);

            //이미지 합성
            for (int y = earY1; y < earY2; y++)
            {
                for (int x = earX1; x < earX2; x++)
                {
                    if (y - earY1 < resizedEar.Rows && x - earX1 < resizedEar.Cols) // 인덱스 범위 체크
                    {
                        //Vec4b earPixel: resizedEar 이미지에서 현재 픽셀의 RGBA 값
                        //Vec4b는 4개의 바이트(각각 Red, Green, Blue, Alpha)를 포함
                        Vec4b earPixel = resizedEar.At<Vec4b>(y - earY1, x - earX1);//위치에 있는 픽셀의 값을 가져옴
                        if (earPixel[3] > 0) // 알파 값이 0이 아닌 픽셀만 처리
                        {
                            Vec3b imgPixel = src.At<Vec3b>(y, x);

                            // 알파 블렌딩
                            // 두 개 이상의 이미지를 결합할 때 사용하는 기술로, 각 이미지의 투명도(알파 값)를 고려하여 합성하는 과정
                            //이미지의 각 픽셀에서 색상과 투명도를 조절하여 최종 이미지를 생성
                            //수식 : 출력 색상=(배경 색상×(255−알파)+전경 색상×알파)/255
                            //*전경 : 합성할 이미지
                            src.At<Vec3b>(y, x) = new Vec3b(
                                (byte)((imgPixel.Item0 * (255 - earPixel[3]) + earPixel[0] * earPixel[3]) / 255),
                                (byte)((imgPixel.Item1 * (255 - earPixel[3]) + earPixel[1] * earPixel[3]) / 255),
                                (byte)((imgPixel.Item2 * (255 - earPixel[3]) + earPixel[2] * earPixel[3]) / 255)
                            );
                        }
                    }
                }
            }
            if (resizedEar != null) resizedEar.Dispose();
        }

        //긴머리, 대머리
        //인수 원본, 전경 이미지 위치, 전경 가로, 전경 세로, 전경 시작좌표 X, Y, 머리 위?
        public Mat faceDeco(Mat src, string path, double faceWidth, double faceHeight, double FX, double FY, bool up)
        {
            decoration = faceDecoPreprocessing(src, path);

            // 얼굴 탐지기 로드
            using (var faceCascade = new CascadeClassifier(filePath))
            {
                OpenCvSharp.Rect[] faces = faceCascade.DetectMultiScale(decoration, scaleFactor: 1.139, minNeighbors: 2);

                foreach (var face in faces)
                {
                    //인수(원본, 전경, 가로, 세로, 시작좌표 x, y, 머리 위?)
                    AlphaBlending(src, face, faceWidth, faceHeight, FX, FY, up);
                }
            }
            return src;
        }

        public void Dispose()
        {
            if (card != null) { card.Dispose(); }
            if(circle != null) { circle.Dispose(); }
            if(coin != null) { coin.Dispose(); }
            if(affine != null ) { affine.Dispose(); }
            if (haarface != null) { haarface.Dispose(); }
            if(decoration != null) { decoration.Dispose(); }
            if(decoImage != null) { decoImage.Dispose(); }
        }
    }
}
