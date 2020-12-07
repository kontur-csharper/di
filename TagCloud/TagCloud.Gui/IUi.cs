using System;
using System.Drawing;
using TagCloud.Gui.InputModels;

namespace TagCloud.Gui
{
    public interface IUi
    {
        void Run();

        UiLockingOperation StartLockingOperation();
        void AddUserInput<T>(UserInputOneOptionChoice<T> inputModel);
        void AddUserInput<T>(UserInputMultipleOptionsChoice<T> inputModel);
        void AddUserInput(UserInputField fieldInput);
        void AddUserInput(UserInputSizeField sizeInput);
        void AddUserInput(UserInputColor colorInput);
        void AddUserInput(UserInputColorPalette colorInput);
        void OnAfterWordDrawn(Image? newImage, Color backgroundColor);

        event Action? ExecutionRequested;
    }
}