using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using static StardewValley.SaveGame;
using static PersistentGameOptions.ModData;


namespace PersistentGameOptions
{
    public class ModEntry : Mod { 
    

        /*********
        ** Public methods
        *********/
        public override void Entry(IModHelper helper)
        {
            ModData GameOptions = helper.ReadConfig<ModData>();
            helper.WriteConfig(GameOptions);

            // get chosen options 
            MenuEvents.MenuClosed += this.MenuEvents_MenuClosed;
            MenuEvents.MenuChanged += this.MenuEvents_MenuChanged;

            // set options upon loading 
            SaveEvents.AfterLoad += this.Load_Options;
            // set options for new game
            MenuEvents.MenuClosed += this.Load_New;
        }

        
        /*********
        ** Private methods
        *********/
        private void MenuEvents_MenuClosed(object sender, EventArgsClickableMenuClosed e)
        {
            if (e.PriorMenu is GameMenu) 
            {
                Save_Options();
            }
        }

        private void MenuEvents_MenuChanged(object sender, EventArgsClickableMenuChanged e)
        {
            if (e.PriorMenu is GameMenu)
            {
                Save_Options();
            }
        }

        private void Load_Options(object sender, EventArgs e)
        {
            ModData gameOptions = this.Helper.ReadConfig<ModData>();
            Game1.options = convertFromModData(gameOptions);
            this.Monitor.Log("Loading saved options.", LogLevel.Info);

        }

        private void Load_New(object sender, EventArgsClickableMenuClosed e)
        {
            if(e.PriorMenu is TitleMenu)
            {
                ModData gameOptions = this.Helper.ReadConfig<ModData>();
                Game1.options = convertFromModData(gameOptions);
                this.Monitor.Log("Loading saved options.", LogLevel.Info);
            }
        }

        private Options convertFromModData(ModData options)
        {
            Options newOptions = new Options();
            newOptions.lightingQuality = options.lightingQuality;
            newOptions.alwaysShowToolHitLocation = options.alwaysShowToolHitLocation;
            return newOptions;
        }

        private void Save_Options()
        {
            Options optionReader = Game1.options;
            ModData temp = new ModData
            {   
                // need to add the rest of options 
                lightingQuality = optionReader.lightingQuality,
                alwaysShowToolHitLocation = optionReader.alwaysShowToolHitLocation
            };
            this.Helper.WriteConfig(temp);
            this.Monitor.Log("Saving options to config.", LogLevel.Info);
        }
    }

}
