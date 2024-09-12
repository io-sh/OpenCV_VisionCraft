using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.InSight.Controls.Display.Internal.OcrMax;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Tesseract;
using static Cognex.InSight.Controls.Display.Internal.RenderText;

namespace openCV0820
{
    internal class OpenCV_Detection : IDisposable
    {
        Mat canny;
        Mat gray;
        Mat corner;
        Mat affine;
        Bitmap tesser;
        Mat circle;
        Mat color;


        public Mat Canny(Mat src)
        {
            canny = new Mat();
            Cv2.Canny(src, canny, 100, 200, 3, true);
            return canny;
        }
        public Mat Corner(Mat src, int size)
        {
            corner = new Mat();
            gray = new Mat();
            Cv2.CopyTo(src, corner);

            //이미 채널이 1이면(그레이스케일이 되어있으면) RGB2GRAY에서 오류가 생김
            if (src.Channels() != 1)  Cv2.CvtColor(src, gray, ColorConversionCodes.RGB2GRAY);
            else Cv2.CopyTo(src, gray);

            Point2f[] corners;
            corners = Cv2.GoodFeaturesToTrack(gray, 100, 0.03, size, null, 5, false, 0);

            for (int i = 0; i < corners.Length; i++)
            {
                OpenCvSharp.Point pt = new OpenCvSharp.Point((int)corners[i].X, (int)corners[i].Y);
                Cv2.Circle(corner, pt, 5, Scalar.Red, 2, LineTypes.AntiAlias);
            }
            return corner;
        }
        public Mat Affine(Mat src, float[] xy)
        {
            affine = new Mat();
            List<Point2f> src_pts = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, src.Height),
                new Point2f(src.Width, 0.0f),
                new Point2f(src.Width, src.Height)   
            };
            List<Point2f> affine_pts = new List<Point2f>()
            {
                new Point2f(xy[0], xy[1]),
                new Point2f(xy[2], xy[3]),
                new Point2f(xy[4], xy[5]),
                new Point2f(xy[6], xy[7])
            };
            Mat matrix = Cv2.GetPerspectiveTransform(affine_pts, src_pts);
            Cv2.WarpPerspective(src, affine, matrix, new OpenCvSharp.Size(src.Width, src.Height));
            return affine;
        }
        public string Tesseract(Mat src)
        {
            tesser = src.ToBitmap();
            var ocr = new TesseractEngine("./tessdata", "kor+eng", EngineMode.Default);
            var texts = ocr.Process(tesser);
            return texts.GetText();
        }
        public Mat Circle(Mat src)
        {
            circle = new Mat();
            Mat dst = new Mat();
            Cv2.CopyTo(src, dst);

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            if (src.Channels() != 1) Cv2.CvtColor(src, circle, ColorConversionCodes.BGR2GRAY);
            else Cv2.CopyTo(src, circle);

            //좀 더 명확한 원 검출을 하기 위해서는 블러처리를 할 필요가 있음
            //블러 처리된 이미지를 사용하거나 중간에 블러처리 실행

            CircleSegment[] circles = Cv2.HoughCircles(circle, HoughModes.Gradient, 1, 100, 100, 35, 0, 0);
            for (int i = 0; i < circles.Length; i++)
            {
                OpenCvSharp.Point center = new OpenCvSharp.Point(circles[i].Center.X, circles[i].Center.Y);

                Cv2.Circle(dst, center, (int)circles[i].Radius, Scalar.White, 3);
                Cv2.Circle(dst, center, 5, Scalar.AntiqueWhite, Cv2.FILLED);
            }
            return dst;
        }

        //중간에 블러처리 하기 -> 좀더 명확히 원을 검출하기 위해
        //블러적용안하면 제대로 안나옴

        public Mat Color(Mat src, string srcColor)
        {
            color = new Mat();
            Mat hsv = new Mat();

            Cv2.CvtColor(src, hsv, ColorConversionCodes.BGR2HSV);
            Mat[] HSV = Cv2.Split(hsv);
            Mat H_color = new Mat();
            switch (srcColor)
            {
                case "red":
                    //0~179, 0~255, 0~255
                    Mat H_color_upRed = new Mat();
                    Mat H_color_downRed = new Mat();
                    Cv2.InRange(HSV[0], new Scalar(0, 100, 100), new Scalar(10, 255, 255), H_color_upRed);
                    Cv2.InRange(HSV[0], new Scalar(178, 100, 100), new Scalar(179, 255, 255), H_color_downRed);
                    Cv2.AddWeighted(H_color_upRed, 1.0, H_color_downRed, 1.0, 0.0, H_color);
                    break;
                case "orange":
                    Cv2.InRange(HSV[0], new Scalar(11), new Scalar(25), H_color);
                    break;
                case "yellow":
                    Cv2.InRange(HSV[0], new Scalar(26), new Scalar(40), H_color);
                    break;
                case "green":
                    Cv2.InRange(HSV[0], new Scalar(41), new Scalar(84), H_color);
                    break;
                case "cyan":
                    Cv2.InRange(HSV[0], new Scalar(85), new Scalar(110), H_color);
                    break;
                case "blue":
                    Cv2.InRange(HSV[0], new Scalar(111), new Scalar(140), H_color);
                    break;
                case "magenta":
                    Cv2.InRange(HSV[0], new Scalar(141), new Scalar(165), H_color);
                    break;
                case "pink":
                    Cv2.InRange(HSV[0], new Scalar(166), new Scalar(177), H_color);
                    break;
                default:
                    MessageBox.Show("색상 범위 오류");
                    break;
            }
            Cv2.BitwiseAnd(hsv, hsv, color, H_color);
            Cv2.CvtColor(color, color, ColorConversionCodes.HSV2BGR);

            return color;
        }
        //색상 백분율 검출


        //사각형검출
       public Mat Square(Mat src)
        {
            OpenCvSharp.Point[] square = FindSquare(src);
            Mat dst = DrawSquare(src, square);
            return dst;
        }
        public static double CalcAngle(OpenCvSharp.Point pt1, OpenCvSharp.Point pt0, OpenCvSharp.Point pt2)
        {
            //cos각도 수식
            double u1 = pt1.X - pt0.X, u2 = pt1.Y - pt0.Y;
            double v1 = pt2.X - pt0.X, v2 = pt2.Y - pt0.Y;

            double numerator = u1 * v1 + u2 * v2;
            double dewnominator = Math.Sqrt(u1*u1+u2*u2)*Math.Sqrt(v1*v1+v2*v2);    
            return numerator / dewnominator;
        }
        public OpenCvSharp.Point[] FindSquare(Mat src)
        {
            Mat[] split = Cv2.Split(src);//bgr채널 나눈뒤 이진화(정확성을 위해)
            Mat blur = new Mat();//정확성을 위한 블러와 이진화
            Mat binary = new Mat();
            OpenCvSharp.Point[] square = new OpenCvSharp.Point[4];//반환할 포인트

            int N = 10; //이진화 종류(정확성을 위해 임계값을 다르게 이진화를 한다.)
            double cos = 1; //사각형 각도
            double max = src.Size().Width * src.Size().Height * 0.9;//입력 이미지의 90%까지의 사각형 이하만 명함으로 인정
            double min = src.Size().Width * src.Size().Height * 0.1;//10%이상만

            for(int channel =0; channel < 3; channel++)//각채널
            {
                Cv2.GaussianBlur(split[channel], blur, new OpenCvSharp.Size(5, 5), 1);//블러처리
                for(int i = 0; i < N; i++)//이진화10개
                {
                    Cv2.Threshold(blur, binary, i * 255 / N, 255, ThresholdTypes.Binary);
                    //윤곽선검출
                    OpenCvSharp.Point[][] contours;//찾은윤곽선 저장 리스트(윤곽선은 점들의 리스트로 표현)
                    HierarchyIndex[] hierarchy;//윤곽선 간 계층 구조(중첩이 어떻게 되어있는지)
                    Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, 
                        ContourApproximationModes.ApproxTC89KCOS);
                    //RetrievalModes.External 윤곽선 검색 종류 : 외곽 윤곽선 검색
                    //ContourApproximationModes.ApproxTC89KCOS 윤곽선 근사화 방법 : Teh - chin 체인 코드 알고리즘으로
                    for (int j=0; j<contours.Length; j++)//찾은 윤곽선 수만큼
                    {
                        //윤곽선 길이 계산 (윤곽선 구성 점 배열, 폐곡선 여부)
                        double perimeter = Cv2.ArcLength(contours[j], true);
                        //윤곽선을 다각형으로 근사화(윤곽선, 길이에 대한 허용 오차, 폐곡선 여부)
                        OpenCvSharp.Point[] result = Cv2.ApproxPolyDP(contours[j], perimeter * 0.02, true);
                        //윤곽선 면적 계산
                        double area = Cv2.ContourArea(result);
                        //윤곽선이 볼록한지 확인
                        bool convex = Cv2.IsContourConvex(result);

                        //사각형 검증
                        if(result.Length == 4&&area> min&& area < max && convex)
                        {
                            double[] angles = new double[4];
                            for(int k =1; k < 5; k++)
                            {
                                double angle = Math.Abs(CalcAngle(result[(k - 1) % 4], result[k % 4], result[(k + 1) % 4]));
                                angles[k-1] = angle;
                            }
                            if (angles.Max() < cos && angles.Max() < 0.15)
                            {
                                cos=angles.Max();
                                square = result;
                            }
                        }
                    }
                }
            }
            return square;
        }
        public Mat DrawSquare(Mat src, OpenCvSharp.Point[] square)
        {
            Mat drawSquare = src.Clone();
            OpenCvSharp.Point[][] pts = new OpenCvSharp.Point[][] { square};
            Cv2.Polylines(drawSquare, pts, true, Scalar.Yellow, 3, LineTypes.AntiAlias, 0);
            return drawSquare;
        }

       

        public void Dispose()
        {
            if(canny != null) canny.Dispose();
            if(gray != null) gray.Dispose();
            if(corner != null) corner.Dispose();  
            if(affine != null) affine.Dispose();
            if(tesser != null) tesser.Dispose();
            if(circle != null) circle.Dispose();
            if(color != null) color.Dispose();
        }
    }
}
