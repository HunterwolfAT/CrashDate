using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CrashDate
{
    class Scripthandler
    {
        Game1 game;

        String scriptName;
        List<String> Script;

        int cPointer = 0;
        bool goToNextCommand = false;
        bool runScript = false;

        Character activechar = null;

        public Scripthandler(Game1 thisgame)
        {
            game = thisgame;
            Script = new List<string>();
        }

        public void PlayScript(String name)
        {
            Script.Clear();
            scriptName = "Content\\Scripts\\" + name + ".cds";
            string line = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(scriptName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        Script.Add(line);
                    }
                }
            }
            catch
            {
                Error("run", "Could not find the script I was supposed to run!");
            }
            runScript = true;
            cPointer = 0;
            goToNextCommand = true;
        }

        public void ReloadScript()
        {
            Script.Clear();
            string line = string.Empty;
            using (StreamReader sr = new StreamReader(scriptName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    Script.Add(line);
                }
            }
        }
        
        public void Update()
        {   
            if (runScript && goToNextCommand)
            {
                ReloadScript();
                if (Script[cPointer].Length > 0)        // Line must not be empty to look for a command
                {
                    // ALL OF THE COMMANDS ARE HERE!
                    // General Commands
                    #region Say
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ SAY COMMAND
                    // Print a MSG in the Textbox.
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer][0] == 's')
                    {
                        String msg = Script[cPointer].Substring(2);
                        msg = msg.Replace("\\n", System.Environment.NewLine);
                        game.gui.WriteMSG(msg);
                        goToNextCommand = false;
                    }
                    #endregion
                    #region Set Background
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ SET THE BACKGROUND
                    // Changes the background image to the named sprite
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0,5) == "bgset")
                    {
                        String newbg = Script[cPointer].Substring(6, Script[cPointer].Length - 6);
                        game.gui.SetBackground(game.Content, newbg);
                    }
                    #endregion
                    #region Fade Background
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FADE TO BACKGROUND
                    // Fades the background image to the named sprite
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 6) == "bgfade")
                    {
                        String newbg = Script[cPointer].Substring(7, Script[cPointer].Length - 7);
                        game.gui.FadeBackground(game.Content, newbg);
                    }
                    #endregion
                    #region Run Script
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ RUN A SCRIPT
                    // Run another script
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 3) == "run")
                    {
                        String nextscript = Script[cPointer].Substring(4, Script[cPointer].Length - 4);
                        PlayScript(nextscript);
                    }
                    #endregion
                    // Characters Commands
                    #region Introduce Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ INTRODUCE CHARACTER
                    // Sets a character active for the scene, making it appear
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 5) == "ichar")
                    {
                        String character = Script[cPointer].Substring(6, Script[cPointer].Length - 6);
                        activechar = game.charmanager.GetChar(character);
                        activechar.active = true;
                    }
                    #endregion
                    #region Fade In Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FADE IN CHARACTER
                    // Sets a character active for the scene, making it appear
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 6) == "fichar")
                    {
                        String character = Script[cPointer].Substring(7, Script[cPointer].Length - 7);
                        activechar = game.charmanager.GetChar(character);
                        activechar.active = true;
                        activechar.Fade(true);
                    }
                    #endregion
                    #region Focus Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FOCUS ON CHARACTER
                    // Makes a character "active" for the script, focussing commands on it
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 5) == "fchar")
                    {
                        String character = Script[cPointer].Substring(6, Script[cPointer].Length - 6);
                        activechar = game.charmanager.GetChar(character);
                    }
                    #endregion
                    #region Change Body
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ CHANGE BODY
                    // Swaps the body of the active character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 4) == "body")
                    {
                        String bodynumber = Script[cPointer].Substring(5, Script[cPointer].Length - 5);
                        if (activechar != null)
                        {
                            int body;
                            int.TryParse(bodynumber, out body);
                            activechar.ChangeBody(body);
                        }
                        else
                            Error("body", "I don't know what character you mean!");
                    }
                    #endregion
                    #region Change Face
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ CHANGE BODY
                    // Swaps the body of the active character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (Script[cPointer].Substring(0, 4) == "face")
                    {
                        String bodynumber = Script[cPointer].Substring(5, Script[cPointer].Length - 5);
                        if (activechar != null)
                        {
                            int face;
                            int.TryParse(bodynumber, out face);
                            activechar.ChangeFace(face);
                        }
                        else
                            Error("face", "I don't know what character you mean!");
                    }
                    #endregion
                }

                if (cPointer < Script.Count - 1)
                    cPointer++;
                else
                {
                    runScript = false;
                    cPointer = 0;
                }
            }
        }

        private void Error(String command, String msg)
        {
            Console.WriteLine("SCRIPT ERROR \"" + command + "\" in Line " + (cPointer + 1).ToString() + ": " + msg );
        }

        public void PushAccept()
        {
            if (runScript)
            {
                if (game.gui.Idle && Script[cPointer - 1][0] == 's')
                {
                    goToNextCommand = true;
                }
            }
        }
    }
}
