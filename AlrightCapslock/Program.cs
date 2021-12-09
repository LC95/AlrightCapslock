// See https://aka.ms/new-console-template for more information
using AlrightCapslock;
using NeatInput.Windows;

var keyboardReceiver = new CapslockReceiver();
var inputSource = new InputSource(keyboardReceiver);
AppDomain.CurrentDomain.ProcessExit += (o, e) =>
{
    inputSource.Dispose();
};
// Starts listening for input
inputSource.Listen();

Console.WriteLine("开启Capslock键移动活动窗口\n关闭Capslock键停止移动\n任意键退出");
Console.ReadKey();
inputSource.Dispose();


