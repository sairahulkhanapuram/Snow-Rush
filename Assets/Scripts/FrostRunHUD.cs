using UnityEngine;
public class FrostRunHUD : MonoBehaviour
{
    private GUIStyle title, body, small, stat, button;
    private Texture2D overlay, statPanel;
    private Texture2D MakeTexture(Color c)
    {
        Texture2D t = new Texture2D(1, 1);
        t.SetPixel(0, 0, c); t.Apply(); return t;
    }
    private void EnsureStyles()
    {
        if (title != null) return;
        title = new GUIStyle(GUI.skin.label) { fontSize = Mathf.Clamp(Screen.width / 25, 30, 58), fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter };
        title.normal.textColor = Color.white;
        body = new GUIStyle(GUI.skin.label) { fontSize = Mathf.Clamp(Screen.width / 65, 17, 25), alignment = TextAnchor.MiddleCenter };
        body.normal.textColor = Color.white;
        small = new GUIStyle(body) { fontSize = Mathf.Clamp(Screen.width / 95, 13, 18) };
        small.normal.textColor = new Color(.84f, .94f, 1f);
        stat = new GUIStyle(small) { fontSize = Mathf.Clamp(Screen.width / 80, 15, 21), fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft };
        stat.normal.textColor = Color.white;
        button = new GUIStyle(GUI.skin.button) { fontSize = Mathf.Clamp(Screen.width / 65, 18, 26), fontStyle = FontStyle.Bold };
        overlay = MakeTexture(new Color(.015f, .045f, .09f, .78f));
        statPanel = MakeTexture(new Color(.015f, .07f, .12f, .76f));
    }
    private void OnGUI()
    {
        EnsureStyles();
        FrostRunGame game = FrostRunGame.Instance;
        if (game == null) return;
        float s = Mathf.Min(Screen.width / 1280f, Screen.height / 720f);
        GUI.DrawTexture(new Rect(16, 16, 245*s, 112*s), statPanel);
        GUI.Label(new Rect(28, 22, 220*s, 27*s), "SCORE   " + game.score.ToString("N0"), stat);
        GUI.Label(new Rect(28, 56, 220*s, 24*s), "CRYSTALS   " + game.coins, small);
        GUI.Label(new Rect(28, 86, 220*s, 24*s), "DISTANCE   " + Mathf.FloorToInt(game.distance) + " m", small);
        if (!game.IsPlaying && !game.IsGameOver)
        {
            DrawOverlay();
            GUI.Label(new Rect(0, Screen.height*.23f, Screen.width, 75), "FROST RUN", title);
            GUI.Label(new Rect(0, Screen.height*.23f+72, Screen.width, 42), "ALPINE RUSH", body);
            GUI.Label(new Rect(0, Screen.height*.52f, Screen.width, 38), "PRESS ENTER OR SPACE TO START", body);
            GUI.Label(new Rect(0, Screen.height*.59f, Screen.width, 30), "A / D or ARROWS: change lane     W / SPACE: jump     S / DOWN: slide", small);
            GUI.Label(new Rect(0, Screen.height*.64f, Screen.width, 28), "Collect golden crystals • Avoid red obstacles", small);
        }
        else if (game.IsPaused)
        {
            DrawOverlay();
            GUI.Label(new Rect(0, Screen.height*.38f, Screen.width, 65), "PAUSED", title);
            GUI.Label(new Rect(0, Screen.height*.49f, Screen.width, 35), "Press ESC to resume", body);
        }
        else if (game.IsGameOver)
        {
            DrawOverlay();
            GUI.Label(new Rect(0, Screen.height*.27f, Screen.width, 70), "RUN OVER", title);
            GUI.Label(new Rect(0, Screen.height*.39f, Screen.width, 38), "Score: " + game.score + "     Crystals: " + game.coins, body);
            GUI.Label(new Rect(0, Screen.height*.45f, Screen.width, 30), "Distance: " + Mathf.FloorToInt(game.distance) + " m", small);
            float w = Mathf.Min(300f, Screen.width*.35f);
            if (GUI.Button(new Rect((Screen.width-w)*.5f, Screen.height*.54f, w, 58f), "NEW GAME", button))
                game.Restart();
        }
    }
    private void DrawOverlay() { GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay); }
}
