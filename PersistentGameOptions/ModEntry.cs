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
    

        public override void Entry(IModHelper helper)
        {
            ModData GameOptions = helper.ReadJsonFile<ModData>($"Data/data.json") ?? new ModData();
            helper.WriteJsonFile($"Data/data.json", GameOptions);

            // get chosen options 
            MenuEvents.MenuClosed += this.MenuEvents_MenuClosed;

            // set options upon loading 
            SaveEvents.AfterLoad += this.Load_Options;
            // set options for new game
            MenuEvents.MenuClosed += this.Load_New;
        }


        private void MenuEvents_MenuClosed(object sender, EventArgsClickableMenuClosed e)
        {
            if (e.PriorMenu is GameMenu) 
            {
                Save_Options();
            }
        }

        private void Load_Options(object sender, EventArgs e)
        {
            ModData gameOptions = this.Helper.ReadJsonFile<ModData>($"Data/data.json");
            Game1.options = convertFromModData(gameOptions);
            this.Monitor.Log("Loading saved options.", LogLevel.Info);

        }

        private void Load_New(object sender, EventArgsClickableMenuClosed e)
        {
            if(e.PriorMenu is TitleMenu)
            {
                ModData gameOptions = this.Helper.ReadJsonFile<ModData>($"Data/data.json");
                Game1.options = convertFromModData(gameOptions);
                this.Monitor.Log("Loading saved options.", LogLevel.Info);
            }
        }

        private Options convertFromModData(ModData options)
        {
            Options newOptions = new Options();
            newOptions.lightingQuality = options.lightingQuality;
            newOptions.alwaysShowToolHitLocation = options.alwaysShowToolHitLocation;
            newOptions.autoRun = options.autoRun;
            newOptions.dialogueTyping = options.dialogueTyping;
            newOptions.windowedBorderlessFullscreen = options.windowedBorderlessFullscreen;
            newOptions.fullscreen = options.fullscreen;
            newOptions.showPortraits = options.showPortraits;
            newOptions.showMerchantPortraits = options.showMerchantPortraits;
            newOptions.showMenuBackground = options.showMenuBackground;
            newOptions.playFootstepSounds = options.playFootstepSounds;
            newOptions.alwaysShowToolHitLocation = options.alwaysShowToolHitLocation;
            newOptions.hideToolHitLocationWhenInMotion = options.hideToolHitLocationWhenInMotion;
            newOptions.pauseWhenOutOfFocus = options.pauseWhenOutOfFocus;
            newOptions.pinToolbarToggle = options.pinToolbarToggle;
            newOptions.showPlacementTileForGamepad = options.showPlacementTileForGamepad;
            newOptions.rumble = options.rumble;
            newOptions.ambientOnlyToggle = options.ambientOnlyToggle;
            newOptions.zoomButtons = options.zoomButtons;
            newOptions.lightingQuality = options.lightingQuality;
            newOptions.invertScrollDirection = options.invertScrollDirection;
            newOptions.screenFlash = options.screenFlash;
            newOptions.hardwareCursor = options.hardwareCursor;
            newOptions.snappyMenus = options.snappyMenus;
            newOptions.snowTransparency = options.snowTransparency;
            newOptions.musicVolumeLevel = options.musicVolumeLevel;
            newOptions.soundVolumeLevel = options.soundVolumeLevel;
            newOptions.zoomLevel = options.zoomLevel;
            newOptions.footstepVolumeLevel = options.footstepVolumeLevel;
            newOptions.ambientVolumeLevel = options.ambientVolumeLevel;
            return newOptions;
        }

        private void Save_Options()
        {
            Options optionReader = Game1.options;
            ModData temp = new ModData
            {
                autoRun = optionReader.autoRun,
                dialogueTyping = optionReader.dialogueTyping,
                windowedBorderlessFullscreen = optionReader.windowedBorderlessFullscreen,
                fullscreen = optionReader.fullscreen,
                showPortraits = optionReader.showPortraits,
                showMerchantPortraits = optionReader.showMerchantPortraits,
                showMenuBackground = optionReader.showMenuBackground,
                playFootstepSounds = optionReader.playFootstepSounds,
                alwaysShowToolHitLocation = optionReader.alwaysShowToolHitLocation,
                hideToolHitLocationWhenInMotion = optionReader.hideToolHitLocationWhenInMotion,
                pauseWhenOutOfFocus = optionReader.pauseWhenOutOfFocus,
                pinToolbarToggle = optionReader.pinToolbarToggle,
                showPlacementTileForGamepad = optionReader.showPlacementTileForGamepad,
                rumble = optionReader.rumble,
                ambientOnlyToggle = optionReader.ambientOnlyToggle,
                zoomButtons = optionReader.zoomButtons,
                lightingQuality = optionReader.lightingQuality,
                invertScrollDirection = optionReader.invertScrollDirection,
                screenFlash = optionReader.screenFlash,
                hardwareCursor = optionReader.hardwareCursor,
                snappyMenus = optionReader.snappyMenus,
                snowTransparency = optionReader.snowTransparency,
                musicVolumeLevel = optionReader.musicVolumeLevel,
                soundVolumeLevel = optionReader.soundVolumeLevel,
                zoomLevel = optionReader.zoomLevel,
                footstepVolumeLevel = optionReader.footstepVolumeLevel,
                ambientVolumeLevel = optionReader.ambientVolumeLevel
            };

            this.Helper.WriteJsonFile($"Data/data.json", temp);
            this.Monitor.Log("Saving options.", LogLevel.Info);
        }
    }

}
