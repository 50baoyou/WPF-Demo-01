using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Wpf.Core.Dialogs
{
    /// <summary>
    /// PopupBox.xaml 的交互逻辑
    /// </summary>
    public partial class PopupBox : Popup
    {
        public PopupBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 透明度
        /// </summary>
        public new double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

        public static new readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof(double), typeof(PopupBox), new PropertyMetadata(1.0, new PropertyChangedCallback(OnPropertyChangedCallback)));

        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PopupBox box && e.NewValue is double v)
            {
                box.Border.Opacity = v;
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(PopupBox), new PropertyMetadata(string.Empty));

        private static readonly PopupBox _dialog = new();
        private static readonly DispatcherTimer _timer = new();

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="owner">所属窗体透明度</param>
        /// <param name="secnds">对话框隐藏倒计时（秒）</param>
        public static void Show(string message, Window? owner = null, double seconds = 1.0)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    owner ??= Application.Current.MainWindow;

                    _dialog.Message = message;
                    _dialog.PlacementTarget = owner; // 确定弹出窗口相对于哪个控件定位
                    _dialog.Placement = PlacementMode.Center;
                    _dialog.StaysOpen = true;
                    _dialog.AllowsTransparency = true;
                    _dialog.Opacity = 0.9;
                    _dialog.IsOpen = true;

                    StartClosePopupTimer(seconds);
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"系统异常：{e.Message}");
            }
        }

        /// <summary>
        /// 开始计时
        /// </summary>
        /// <param name="seconds"></param>
        private static void StartClosePopupTimer(double seconds)
        {

            _timer.Interval = TimeSpan.FromSeconds(seconds);
            _timer.Tick -= ClosePopupHandler; // 取消订阅
            _timer.Tick += ClosePopupHandler; // 订阅事件
            _timer.Start();
        }

        private static void ClosePopupHandler(object? sender, EventArgs e)
        {
            _timer?.Stop();
            ClosePopup();
        }

        /// <summary>
        /// 关闭弹窗，动画时间1秒
        /// </summary>
        private static void ClosePopup()
        {
            _dialog.Dispatcher.Invoke(() =>
            {
                // 创建动画
                var animation = new DoubleAnimation
                {
                    From = _dialog.Opacity,
                    To = 0.0,
                    Duration = TimeSpan.FromSeconds(1),
                    FillBehavior = FillBehavior.Stop
                };

                // 动画完成事件
                animation.Completed += (s, e) =>
                {
                    _dialog.IsOpen = false;
                    _dialog.Message = string.Empty;

                    // 清理动画完成事件避免内存泄漏
                    animation.Completed -= (sender, args) => { };
                };

                // 开始动画
                _dialog.BeginAnimation(OpacityProperty, animation);
            });
        }

    }
}