using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

public class FileEditorScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();
        Core.ExitOnEscape = false;
        InitializeUI();
    }

    public override void LoadContent()
    {
    }

    public override void Update(GameTime gameTime)
    {
        Gum.GumService.Default.Update(gameTime);

        CheckKeyboardInput();
        CheckGamePadInput();
    }

    private void CheckKeyboardInput()
    {
        KeyboardInfo keyboard = Core.Input.Keyboard;
    }

    private void CheckGamePadInput()
    {
        GamePadInfo gamePadOne = Core.Input.GamePads[(int)PlayerIndex.One];
    }

    private void InitializeUI()
    {
        Gum.GumService.Default.Root.Children.Clear();
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.CornflowerBlue);
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Core.SpriteBatch.End();
        Gum.GumService.Default.Draw();
    }

}
