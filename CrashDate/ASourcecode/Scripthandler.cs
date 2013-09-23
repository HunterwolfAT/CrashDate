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

        int selectedchoice = -1;

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
                runScript = true;
                cPointer = 0;
                goToNextCommand = true;
            }
            catch
            {
                Error("run", "Could not find the script I was supposed to run!");
                runScript = false;
            }

            if (Script.Count() == 0)
            {
                runScript = false;
                Console.WriteLine("Your Script is fucking empty!");
            }
            
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
                    // Remove empty spaces that come before the command
                    Script[cPointer] = Script[cPointer].Trim();

                    String[] words = Script[cPointer].Split(' ');

                    if (activechar == null)
                        activechar = game.charmanager.GetChar("Spieler");

                    // ALL OF THE COMMANDS ARE HERE!
                    // General Commands
                    #region Say
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ SAY COMMAND
                    // Print a MSG in the Textbox.
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "s" || words[0] == "say")
                    {
                        String msg = "";
                        int startingpoint = 1;

                        // if necessary, change active character first
                        if (words[1] == "fchar" || words[1] == "focuschar" || words[1] == "fc")
                        {
                            if (words[2] == "player" || words[2] == "p")
                            {
                                activechar = game.charmanager.GetChar("Spieler");
                            }
                            else
                            {
                                String character = words[2];
                                activechar = game.charmanager.GetChar(character);
                            }
                            startingpoint = 3;
                        }

                        for (int x = startingpoint; x < words.Count(); x++)
                            msg += words[x] + " ";
                        msg = msg.Replace("\\n", System.Environment.NewLine);
                        game.gui.WriteMSG(activechar.name, msg);
                        goToNextCommand = false;
                    }
                    #endregion
                    #region Run Script
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ RUN A SCRIPT
                    // Run another script
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "run")
                    {
                        String nextscript = words[1];
                        PlayScript(nextscript);
                    }
                    #endregion
                    #region Present Choice
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ PRESENT A CHOICE
                    // Let the Player decide a path
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "choice")
                    {
                        // Display the choices
                        if (words[1] == "ask")
                        {
                            game.gui.ShowChoices();
                            goToNextCommand = false;
                        }
                        else if (words[1] == "case")    // The Player selected one choice, so jump to that line in the script
                        {
                            if (words[2] != "end")
                            {
                                int answer = -1;
                                int.TryParse(words[2], out answer);
                                if (answer != selectedchoice)
                                {
                                    JumpToNext("choice case");
                                }
                            }
                            else
                            {
                                selectedchoice = -1;
                            }
                        }
                        else    // Set up the choices 
                        {
                            String question = "";
                            for (int x = 1; x < words.Count(); x++)
                                question += words[x] + " ";
                            game.gui.AddQuestion(question);
                        }

                    }
                    #endregion
                    #region Set Background
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ SET THE BACKGROUND
                    // Changes the background image to the named sprite
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "bgset" || words[0] == "backgroundset")
                    {
                        String newbg = words[1];
                        game.gui.SetBackground(game.Content, newbg);
                    }
                    #endregion
                    #region Fade Background
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FADE TO BACKGROUND
                    // Fades the background image to the named sprite
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "bgfade" || words[0] == "backgroundfade")
                    {
                        String newbg = words[1];
                        game.gui.FadeBackground(game.Content, newbg);
                    }
                    #endregion
                    #region Check Sympathy
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ CHECK A CHARS SYMPATHY
                    // Checks the active chars sympathy value and branches the script
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "checksympathy")
                    {
                        String operat0r = words[1];
                        String valuestr = words[2];
                        int valueint;
                        int.TryParse(valuestr, out valueint);

                        if (operat0r == "bigger")
                        {
                            if (activechar.GetSympathy() <= valueint)
                            {
                                JumpToNext("endcheck");
                            }
                        }
                        else if (operat0r == "smaller")
                        {
                            if (activechar.GetSympathy() >= valueint)
                            {
                                JumpToNext("endcheck");
                            }
                        }
                        else if (operat0r == "equal")
                        {
                            if (activechar.GetSympathy() != valueint)
                            {
                                JumpToNext("endcheck");
                            }
                        }
                        else if (operat0r == "notequal")
                        {
                            if (activechar.GetSympathy() == valueint)
                            {
                                JumpToNext("endcheck");
                            }
                        }
                        else if (operat0r == "smallerequal")
                        {
                            if (activechar.GetSympathy() > valueint)
                            {
                                JumpToNext("endcheck");
                            }
                        }
                        else if (operat0r == "biggerequal")
                        {
                            if (activechar.GetSympathy() < valueint)
                            {
                                JumpToNext("endcheck");
                            }
                        }
                        else
                        {
                            Error("checksympathy", "Operator must be either 'equal' 'bigger' 'biggerequal' 'smaller' 'smallerequal' or 'notequal'");
                        }
                    }
                    #endregion
                    // Audio Commands
                    #region Play Song
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ PLAY A BACKGROUNDSONG
                    // Play a Backgroundsong
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "song")
                    {
                        String songname = "";
                        for (int x = 1; x < words.Count(); x++)
                        {
                            songname += words[x];
                            if (x < words.Count() - 1)
                                songname += " ";
                        }
                        game.audio.PlaySong(songname);
                    }
                    #endregion
                    #region Play SFX
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ PLAY A SOUNDEFFECT
                    // Play a Soundeffect
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "sfx")
                    {
                        String sfxname = "";
                        for (int x = 1; x < words.Count(); x++)
                        {
                            sfxname += words[x];
                            if (x < words.Count() - 1)
                                sfxname += " ";
                        }
                        game.audio.PlaySFX(sfxname, game.Content);
                    }
                    #endregion
                    // Characters Commands
                    #region Introduce Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ INTRODUCE CHARACTER
                    // Sets a character active for the scene, making it appear
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "ichar" || words[0] == "introducechar")
                    {
                        String character = words[1];
                        activechar = game.charmanager.GetChar(character);
                        activechar.active = true;
                    }
                    #endregion
                    #region Fade In Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FADE IN CHARACTER
                    // Sets a character active for the scene, making it appear
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "fichar" || words[0] == "fadeinchar")
                    {
                        String character;

                        // if necessary, change active character first
                        if (words[1] == "fchar" || words[1] == "focuschar" || words[1] == "fc")
                        {
                            character = words[2];
                        }
                        else
                        {
                            character = words[1];   
                        }

                        // If there is already one char in screen, change the layout
                        if (game.charmanager.NumberActiveChars() >= 1)
                        {
                            //Console.WriteLine("More than one character is already on screen. Moving to a TWO CHAR LAYOUT.");
                            //Console.WriteLine(activechar.name);
                            //Console.WriteLine(game.charmanager.GetChar(character).name);

                            //activechar.Move(290);   // Move the old active char to the left
                            //// New active char
                            //activechar = game.charmanager.GetChar(character);
                            //activechar.active = true;
                            //activechar.Fade(true);
                            //activechar.Move(790);
                            game.charmanager.TwoCharLayout(game.charmanager.GetChar(character));
                            activechar = game.charmanager.GetChar(character);
                            activechar.active = true;
                            activechar.SetOpacity(255);
                        }
                        else    //If there is no char on screen yet
                        {
                            activechar = game.charmanager.GetChar(character);
                            activechar.active = true;
                            activechar.reset();
                            activechar.Fade(true);
                        }
                        
                    }
                    #endregion
                    #region Fade Out Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FADE OUT CHARACTER
                    // Removes a character from the scene, making it disappear
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "fochar")
                    {
                        String character;

                        // if necessary, change active character first
                        if (words[1] == "fchar" || words[1] == "focuschar" || words[1] == "fc")
                        {
                            character = words[2];
                        }
                        else
                        {
                            character = words[1];
                        }

                        activechar = game.charmanager.GetChar(character);

                        if (game.charmanager.NumberActiveChars() > 1)
                        {
                            game.charmanager.OneCharLayout(activechar);
                            activechar.active = false;
                        }
                        else    //If there is only one char on screen
                        {
                            activechar.Fade(false);
                            activechar.active = false;
                        }
                    }
                    #endregion
                    #region Focus Character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ FOCUS ON CHARACTER
                    // Makes a character "active" for the script, focussing commands on it
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "fchar" || words[0] == "focuschar" || words[0] == "fc")
                    {
                        if (words[1] == "player" || words[1] == "p")
                        {
                            activechar = game.charmanager.GetChar("Spieler");
                        }
                        else
                        {
                            String character = words[1];
                            activechar = game.charmanager.GetChar(character);
                        }
                    }
                    #endregion
                    #region Change Body
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ CHANGE BODY
                    // Swaps the body of the active character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "body")
                    {
                        String bodynumber = words[1];
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
                    if (words[0] == "face")
                    {
                        String bodynumber = words[1];
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
                    #region Change Sympathy
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ CHANGE SYMPATHY
                    // Adds to or takes from the "I like you"-parameter of the active character
                    // \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                    if (words[0] == "sympathy")
                    {
                        String option = words[1];
                        String valuestr = words[2];
                        if (activechar != null)
                        {
                            int valueint;
                            int.TryParse(valuestr, out valueint);
                            if (option == "inc" || option == "+")
                            {
                                activechar.IncreaseSympathy(valueint);
                            }
                            else if (option == "dec" || option == "-")
                            {
                                activechar.DecreaseSympathy(valueint);
                            }
                            else
                            {
                                Error("sympathy", "The first parameter should either be 'inc' or 'dec'!");
                            }
                            Console.WriteLine(activechar.GetSympathy());
                        }
                        else
                            Error("sympathy", "I don't know what character you mean!");
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

        private void JumpToNext(String parse)
        {
            int choicestack = 0;
            
            for (int x = cPointer + 1; x < Script.Count(); x++)
            {
                Console.WriteLine(Script[x]);
                //if (Script[cPointer - 1].Substring(0, 6) == "choice")
                //{
                    if (Script[x].Contains("choice ask"))
                    {
                        choicestack++;
                        Console.WriteLine("Found a choise ask! Increasing choicestack!");
                    }
                    else if (Script[x].Contains("choice case end") && choicestack > 0)
                    {
                        choicestack--;
                        Console.WriteLine("Found a choise end! Decreasing choicestack!");
                    }
                //}

                if (Script[x].Contains(parse) && choicestack == 0)
                {
                    cPointer = x;
                    break;
                }
            }
        }

        public void PushAccept(int choice)
        {
            if (runScript)
            {
                if (!game.gui.showchoice && game.gui.Idle && Script[cPointer - 1][0] == 's')
                {
                    goToNextCommand = true;
                }

                if (Script[cPointer - 1].Length >= 6)
                {
                    if (Script[cPointer - 1].Substring(0, 6) == "choice" && game.gui.showchoice)
                    {
                        selectedchoice = choice + 1;        // Lets not make it start at 0
                        //Console.WriteLine(selectedchoice);
                        goToNextCommand = true;
                        game.gui.CleanUpChoices();
                    }
                }
            }
        }

        public void ExportScript()
        {
            Console.WriteLine("Exporting script: " + scriptName);
            int lineNumber = 1;

            for (int sPointer = 0; sPointer < Script.Count(); sPointer++)
            {
                String[] words = Script[sPointer].Split(' ');

                if (words[0] == "s" || words[0] == "say")
                {
                    String msg = "";
                    int startingpoint = 1;

                    for (int x = startingpoint; x < words.Count(); x++)
                    {
                        if (words[x] != "fchar" && words[x] != "focuschar" && words[x] != "fc")
                        {

                        }
                        else if (words[x] != "player" && words[x] != "p" && game.charmanager.GetChar(words[x]) == null)
                        {
                            msg += words[x] + " ";
                        }
                    }
                    msg = msg.Replace("\\n", System.Environment.NewLine);

                    Console.WriteLine(lineNumber + ": " + msg);
                    lineNumber++;
                }
            }
        }
    }
}
