using Perspex;
using Perspex.Media;
using Perspex.Platform;

namespace Tuner.Perspex
{
    public class ArcSegmentGeometry : Geometry
    {
        private readonly Point _startPoint;
        private readonly Point _point;
        private readonly Size _size;
        private readonly double _rotationAngle;
        private readonly bool _isLargeArc;
        private readonly SweepDirection _sweepDirection;
        private readonly bool _isClosed;
        private readonly bool _isStroked;

        public ArcSegmentGeometry(Point startPoint, Point point, Size size, double rotationAngle, bool isLargeArc, SweepDirection sweepDirection,bool isClosed,bool isStroked)
        {
            _startPoint = startPoint;
            _point = point;
            _size = size;
            _rotationAngle = rotationAngle;
            _isLargeArc = isLargeArc;
            _sweepDirection = sweepDirection;
            _isClosed = isClosed;
            _isStroked = isStroked;
            IPlatformRenderInterface factory = PerspexLocator.Current.GetService<IPlatformRenderInterface>();
            IStreamGeometryImpl impl = factory.CreateStreamGeometry();

            using (IStreamGeometryContextImpl context = impl.Open())
            {
                context.BeginFigure(startPoint, isStroked);
                context.ArcTo(point, size, rotationAngle,isLargeArc, sweepDirection);
                context.EndFigure(isClosed);
            }

            PlatformImpl = impl;
        }

        /// <inheritdoc/>
        public override Rect Bounds => PlatformImpl.Bounds;

        /// <inheritdoc/>
        public override Geometry Clone()
        {
            return new ArcSegmentGeometry(_startPoint,_point, _size,_rotationAngle,_isLargeArc,_sweepDirection, _isClosed, _isStroked);
        }
    }
}
