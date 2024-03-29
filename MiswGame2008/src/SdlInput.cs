using System;
using Yanesdk.Draw;
using Yanesdk.Ytl;
using Yanesdk.Input;

namespace MiswGame2008
{
    public class SdlInput : IInput
    {
        private KeyBoardInput keyBoardInput;
        private JoyStick joyStick;
        private MouseInput mouseInput;

        public SdlInput(bool fullscreen)
        {
            keyBoardInput = new KeyBoardInput();
            joyStick = new JoyStick(0);
            mouseInput = new MouseInput();
            if (fullscreen)
            {
                mouseInput.Hide();
            }
        }

        public void Update()
        {
            keyBoardInput.Update();
            joyStick.Update();
            mouseInput.Update();
        }

        public bool IsPressLeft
        {
            get
            {
                return keyBoardInput.IsPress(KeyCode.LEFT)
                    || keyBoardInput.IsPress(KeyCode.KP4)
                    || joyStick.IsPress(2);
            }
        }

        public bool IsPressUp
        {
            get
            {
                return keyBoardInput.IsPress(KeyCode.UP)
                    || keyBoardInput.IsPress(KeyCode.KP8)
                    || joyStick.IsPress(0);
            }
        }

        public bool IsPressRight
        {
            get
            {
                return keyBoardInput.IsPress(KeyCode.RIGHT)
                    || keyBoardInput.IsPress(KeyCode.KP6)
                    || joyStick.IsPress(3);
            }
        }

        public bool IsPressDown
        {
            get
            {
                return keyBoardInput.IsPress(KeyCode.DOWN)
                    || keyBoardInput.IsPress(KeyCode.KP2)
                    || joyStick.IsPress(1);
            }
        }

        public bool IsPressButton1
        {
            get
            {
                return keyBoardInput.IsPress(KeyCode.a)
                    || keyBoardInput.IsPress(KeyCode.z)
                    || keyBoardInput.IsPress(KeyCode.SPACE)
                    || keyBoardInput.IsPress(KeyCode.LCTRL)
                    || keyBoardInput.IsPress(KeyCode.RCTRL)
                    || IsJoyStickButtonPress();
            }
        }

        public bool IsPressButton2
        {
            get
            {
                return keyBoardInput.IsPress(KeyCode.s)
                    || keyBoardInput.IsPress(KeyCode.x)
                    || keyBoardInput.IsPress(KeyCode.RETURN)
                    || keyBoardInput.IsPress(KeyCode.LSHIFT)
                    || keyBoardInput.IsPress(KeyCode.RSHIFT);
            }
        }

        public bool IsPushLeft
        {
            get
            {
                return keyBoardInput.IsPush(KeyCode.LEFT)
                    || keyBoardInput.IsPush(KeyCode.KP8)
                    || joyStick.IsPush(2);
            }
        }

        public bool IsPushUp
        {
            get
            {
                return keyBoardInput.IsPush(KeyCode.UP)
                    || keyBoardInput.IsPush(KeyCode.KP8)
                    || joyStick.IsPush(0);
            }
        }

        public bool IsPushRight
        {
            get
            {
                return keyBoardInput.IsPush(KeyCode.RIGHT)
                    || keyBoardInput.IsPush(KeyCode.KP6)
                    || joyStick.IsPush(3);
            }
        }

        public bool IsPushDown
        {
            get
            {
                return keyBoardInput.IsPush(KeyCode.DOWN)
                    || keyBoardInput.IsPush(KeyCode.KP2)
                    || joyStick.IsPush(1);
            }
        }

        public bool IsPushButton1
        {
            get
            {
                return keyBoardInput.IsPush(KeyCode.a)
                    || keyBoardInput.IsPush(KeyCode.z)
                    || keyBoardInput.IsPush(KeyCode.SPACE)
                    || keyBoardInput.IsPush(KeyCode.LCTRL)
                    || keyBoardInput.IsPush(KeyCode.RCTRL)
                    || IsJoyStickButtonPush();
            }
        }

        public bool IsPushButton2
        {
            get
            {
                return keyBoardInput.IsPush(KeyCode.s)
                    || keyBoardInput.IsPush(KeyCode.x)
                    || keyBoardInput.IsPush(KeyCode.RETURN)
                    || keyBoardInput.IsPush(KeyCode.LSHIFT)
                    || keyBoardInput.IsPush(KeyCode.RSHIFT);
            }
        }

        public UserCommand UserCommand
        {
            get
            {
                if (!SDLFrame.IsActive)
                {
                    return UserCommand.Empty;
                }
                return new UserCommand(
                    IsPushLeft,
                    IsPushUp,
                    IsPushRight,
                    IsPushDown,
                    IsPushButton1,
                    IsPushButton2,
                    keyBoardInput.IsPush(KeyCode.ESCAPE));
            }
        }

        public GameCommand GameCommand
        {
            get
            {
                if (!SDLFrame.IsActive)
                {
                    return GameCommand.Empty;
                }
                return new GameCommand(
                    IsPressLeft,
                    IsPressUp,
                    IsPressRight,
                    IsPressDown,
                    IsPressButton1,
                    IsPressButton2,
                    keyBoardInput.IsPush(KeyCode.ESCAPE));
            }
        }

        private bool IsJoyStickButtonPress()
        {
            for (int i = 4; i < joyStick.ButtonNum; i++)
            {
                if (joyStick.IsPress(i))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsJoyStickButtonPush()
        {
            for (int i = 4; i < joyStick.ButtonNum; i++)
            {
                if (joyStick.IsPush(i))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
