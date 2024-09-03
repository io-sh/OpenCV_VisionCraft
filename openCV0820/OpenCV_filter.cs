using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp.Extensions;

namespace openCV0820
{
    internal class OpenCV_filter : IDisposable
    {
        Mat gray;//이진화
        Mat bin;//빈 값

        //모폴로지
        Mat dil;//팽창
        Mat ero;//침식
        //연산
        Mat gradient;
        Mat tophat;
        Mat blackhat;

        //블러
        Mat blur;
        Mat gaussian;
        public Mat GrayScale(Mat src)
        {
            gray = new Mat();
            //이미 채널이 1이면(그레이스케일이 되어있으면) RGB2GRAY에서 오류가 생김
            if (src.Channels() != 1)
            {
                Cv2.CvtColor(src, gray, ColorConversionCodes.RGB2GRAY);
                return gray;
            }
            else
                return src;
        }
        public Mat Binary(Mat src, int TH)
        {
            bin = new Mat();
            Cv2.Threshold(src, bin, TH, 255, ThresholdTypes.Binary);
            return bin;
        }
        public Mat Dil(Mat src, int count)
        {
            dil = new Mat();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Cross, new Size(5,5));
            Cv2.Dilate(src, dil, element, new Point(-1, -1), count);
            return dil; 
        }
        public Mat Ero(Mat src, int count) 
        {
            ero = new Mat();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Cross, new Size(5, 5));
            Cv2.Erode(src, ero, element, new Point(-1, -1), count);
            return ero;
        }
        //두개 한눈에 보기
        public Mat DilEro(Mat src)
        {
            bin = new Mat();
            dil = new Mat();
            ero = new Mat();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Cross, new Size(5, 5));

            Cv2.Dilate(src, dil, element, new Point(2, 2), 3);
            Cv2.Erode(src, ero, element, new Point(2, 2), 3);
            //옆으로 연결(이미지 두개)
            Cv2.HConcat(new Mat[] { dil, ero }, bin);
            return bin;
        }
        public Mat Gradient(Mat src)
        {
            gradient = new Mat();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(5, 5));
            Cv2.MorphologyEx(src, gradient, MorphTypes.Gradient, element, new Point(-1, -1), 3);
            return gradient;
        }
        public Mat Tophat(Mat src)
        {
            tophat = new Mat();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(5, 5));
            Cv2.MorphologyEx(src, tophat, MorphTypes.TopHat, element, new Point(-1, -1), 3);
            return tophat;
        }
        public Mat Blackhat(Mat src)
        {
            blackhat = new Mat();
            Mat element = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(5, 5));
            Cv2.MorphologyEx(src, blackhat, MorphTypes.BlackHat, element, new Point(-1, -1), 3);
            return blackhat;
        }

        public Mat Blur(Mat src, int size)
        {
            blur = new Mat();
            Cv2.Blur(src, blur, new Size(size, size), new Point(-1, -1), BorderTypes.Default);
            return blur;    
        }
        public Mat Gaussian(Mat src, int size)
        {
            gaussian = new Mat();
            Cv2.GaussianBlur(src, gaussian, new Size(size, size), 0,0, BorderTypes.Default);
            return gaussian;
        }
        public void Dispose()
        {
            if (gray != null) gray.Dispose();
            if(dil != null) dil.Dispose();
            if(ero != null) ero.Dispose();
            if(gradient != null) gradient.Dispose();
            if(tophat != null) tophat.Dispose();
            if(blackhat != null) blackhat.Dispose();
            if(blur != null) blur.Dispose();
            if(gaussian != null) gaussian.Dispose();
        }
    }
}
