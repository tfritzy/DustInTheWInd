using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameLibrary;

public class Game1 : Core
{
    public Game1() : base("Dust in the Wind - Editor", 1920, 1080, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();
        InitializeGum();
        ChangeScene(new FileEditorScene());
    }

    private void InitializeGum()
    {
        Gum.GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
        Gum.GumService.Default.ContentLoader.XnaContentManager = Core.Content;
        FrameworkElement.KeyboardsForUiControl.Add(Gum.GumService.Default.Keyboard);
        FrameworkElement.GamePadsForUiControl.AddRange(Gum.GumService.Default.Gamepads);
        FrameworkElement.TabReverseKeyCombos.Add(
           new KeyCombo() { PushedKey = Gum.Forms.Input.Keys.Up });
        FrameworkElement.TabKeyCombos.Add(
           new KeyCombo() { PushedKey = Gum.Forms.Input.Keys.Down });
        Gum.GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / 4.0f;
        Gum.GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / 4.0f;
        Gum.GumService.Default.Renderer.Camera.Zoom = 4.0f;
    }

    protected override void LoadContent()
    {
    }
}
