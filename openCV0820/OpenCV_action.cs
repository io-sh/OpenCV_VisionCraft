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
using Tesseract;
using static System.Net.Mime.MediaTypeNames;

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
            MessageBox.Show(square[0].X+"  "+ square[0].Y + "  "+ square.Length);
            MessageBox.Show(square[1].X + "  " + square[1].Y + "  " + square.Length);
            MessageBox.Show(square[2].X + "  " + square[2].Y + "  " + square.Length);
            MessageBox.Show(square[3].X + "  " + square[3].Y + "  " + square.Length);
            //OpenCvSharp.Point[][] pts = new OpenCvSharp.Point[][] { square };
            List<Point2f> src_pts = new List<Point2f>()
            {
                new Point2f(0.0f, 0.0f),
                new Point2f(0.0f, src.Height),
                new Point2f(src.Width, 0.0f),
                new Point2f(src.Width, src.Height)
            };

            square = sortPoint(square);
            MessageBox.Show(square[0].X + "  " + square[0].Y + "  " + square.Length);
            MessageBox.Show(square[1].X + "  " + square[1].Y + "  " + square.Length);
            MessageBox.Show(square[2].X + "  " + square[2].Y + "  " + square.Length);
            MessageBox.Show(square[3].X + "  " + square[3].Y + "  " + square.Length);
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

        public void Dispose()
        {
            if (card != null) { card.Dispose(); }
            if(circle != null) { circle.Dispose(); }
            if(coin != null) { coin.Dispose(); }
            if(affine != null ) { affine.Dispose(); }
        }
    }
}
