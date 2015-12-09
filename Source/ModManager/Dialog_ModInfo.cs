// ModManager/Dialog_ModInfo.cs
// 
// Copyright Karel Kroeze, 2015.
// 
// Created 2015-12-09 13:24

using RimWorld;
using System;
using UnityEngine;
using Verse;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EdBFluffy.ModOrderWithVersionChecking
{
    // A dialog that displays all of the information about the mod (author, description,
    // URL, etc.) that is normally displayed in the main Mods config screen in the vanilla
    // game.  We don't have room for it in the modded version of the screen, so we put it
    // in a dialog.
    public class Dialog_ModInfo : Window
    {
        protected static float Margin = 24;
        protected static Vector2 WinSize = new Vector2( 640, 640 );
        protected static Vector2 ContentSize = new Vector2( WinSize.x - Margin * 2, WinSize.y - Margin * 2 );
        protected InstalledMod mod;

        public override Vector2 InitialWindowSize
        {
            get { return WinSize; }
        }

        // The constructor takes the mod for which it will display information as an argument.
        public Dialog_ModInfo( InstalledMod mod )
        {
            // Standard configuration from the base class.
            doCloseButton = true;

            // Store the mod.
            this.mod = mod;
        }

        // DEBUG: print version data.
        public override void PreOpen()
        {
            base.PreOpen();
            VersionData version = Page_ModsConfig.localVersionData[mod.Identifier];
            Log.Message( "Current version: " + version.version + 
                         "\n Version URL: " + version.versionURL + 
                         "\n Date: " + version.date );
        }

        // Draw the dialog contents.
        public override void DoWindowContents( Rect inRect )
        {
            float width = ContentSize.x;
            float height = ContentSize.y;
            Text.Font = GameFont.Medium;
            Rect labelRect = new Rect( 0, 0, width, 40 );
            Widgets.Label( labelRect, mod.Name );
            Rect imageRect = new Rect( 0, labelRect.yMax, 0, 20 );
            if ( mod.previewImage != null )
            {
                imageRect.width = mod.previewImage.width;
                imageRect.height = mod.previewImage.height;
                imageRect.x = width / 2 - imageRect.width / 2;
                GUI.DrawTexture( imageRect, mod.previewImage, ScaleMode.ScaleToFit );
            }
            Text.Font = GameFont.Small;
            float imageHeight = imageRect.yMax + 15;
            Rect descriptionRect = new Rect( 0, imageHeight, width, height - imageHeight );
            string fullDescriptionText = string.Concat( "Author".Translate(), ": ", mod.Author, "\n\n", mod.Description );
            Widgets.Label( descriptionRect, fullDescriptionText );
            if ( mod.Url != string.Empty )
            {
                float x = Text.CalcSize( mod.Url ).x;
                Rect urlRect = new Rect( descriptionRect );
                urlRect.xMin += urlRect.xMax - x;
                urlRect.height = 25;
                if ( Widgets.TextButton( urlRect, mod.Url, false, true ) )
                {
                    Application.OpenURL( mod.Url );
                }
            }
        }
    }
}