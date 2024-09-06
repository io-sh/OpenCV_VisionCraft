using System;
using OpenCvSharp;

using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
namespace openCV0820
{
    internal class OpenCV_CLASS: IDisposable
    {
        Mat symn;//대칭 변수
        Mat zoomin;//확대 축소
        Mat zoomout;
        Mat resize;//크기조절
        Mat cut;//자르기//crop로 이름 바꾸기, 아니면 삭제
        Mat rotation;

        //대칭
        public Mat SymmetryY(Mat src)
        {
            symn = new Mat();
            Cv2.Flip(src, symn, FlipMode.Y);
            return symn;
        }
        public Mat SymmetryX(Mat src)
        {
            symn = new Mat();
            Cv2.Flip(src, symn, FlipMode.X);
            return symn;
        }

        //확대축소
        public Mat ZoomIn(Mat src)
        { 
            zoomin = new Mat();
            Cv2.PyrUp(src, zoomin);
            return zoomin;
        }
        public Mat ZoomOut(Mat src)
        {
            zoomout = new Mat();
            Cv2.PyrDown(src, zoomout);
            return zoomout;
        }

        //크기조절
        public Mat ResizeImage(Mat src, int size)
        {
            resize = new Mat();
            Cv2.Resize(src, resize, new Size(src.Width* size / 100, src.Height* size / 100));
            return resize;
        }

        public Mat Cutting(Mat src, int pointX, int pointY, int pointW, int pointH)
        {
            cut = src.SubMat(new Rect(pointX,pointY,pointW,pointH));
            return cut;
        }
        public Mat Rotation(Mat src, int angle)
        {
            rotation = new Mat();
            Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(src.Width / 2, src.Height / 2), angle, 1.0);
            Cv2.WarpAffine(src, rotation, matrix, new Size(src.Width, src.Height));
            return rotation;
        }

        //종료
        public void Dispose()
        {
            if(symn != null) symn.Dispose();
            if(zoomin != null) zoomin.Dispose();
            if (zoomout != null) zoomout.Dispose();
            if (resize != null) resize.Dispose();
            if(cut != null) cut.Dispose();
            if(rotation != null) rotation.Dispose();
        }
    }
}
