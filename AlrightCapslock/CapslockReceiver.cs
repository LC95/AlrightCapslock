using NeatInput.Windows;
using NeatInput.Windows.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlrightCapslock
{
    internal class CapslockReceiver : IKeyboardEventReceiver
    {
        public void Receive(KeyboardEvent @event)
        {
            //启动Capslock
            if (@event.Key == NeatInput.Windows.Processing.Keyboard.Enums.Keys.CapsLock
                && @event.State == NeatInput.Windows.Processing.Keyboard.Enums.KeyStates.Down
                && !Console.CapsLock)
            {
                WindowMoveHelper.Begin();
            }
            //取消Capslock
            if (@event.Key == NeatInput.Windows.Processing.Keyboard.Enums.Keys.CapsLock
                && @event.State == NeatInput.Windows.Processing.Keyboard.Enums.KeyStates.Down
                && Console.CapsLock)
            {
                WindowMoveHelper.Stop();
            }
        }
    }
}
