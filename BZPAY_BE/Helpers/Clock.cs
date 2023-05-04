namespace BZPAY_BE.DataAccess
{
    public class Clock : IDisposable
    {
        private static DateTime? _nowForTest;

        public static DateTime Now
        {
            get { return _nowForTest ?? DateTime.Now; }
        }

        public static IDisposable NowIs(DateTime dateTime)
        {
            _nowForTest = dateTime;
            return new Clock();
        }

        public void Dispose()
        {
            _nowForTest = null;
        }
    };
}