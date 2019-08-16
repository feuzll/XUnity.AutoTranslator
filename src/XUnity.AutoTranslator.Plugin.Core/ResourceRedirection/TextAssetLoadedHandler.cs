﻿using System.IO;
using System.Text;
using UnityEngine;
using XUnity.AutoTranslator.Plugin.Core.Utilities;
using XUnity.Common.Utilities;
using XUnity.ResourceRedirector;

namespace XUnity.AutoTranslator.Plugin.Core.ResourceRedirection
{
   internal class TextAssetLoadedHandler : AssetLoadedHandlerBase<TextAsset>
   {
      protected override string CalculateModificationFilePath( AssetLoadedContext<TextAsset> context )
      {
         return context.GetPreferredFilePath( ".txt" );
      }

      protected override bool DumpAsset( string calculatedModificationPath, AssetLoadedContext<TextAsset> context )
      {
         Directory.CreateDirectory( new FileInfo( calculatedModificationPath ).Directory.FullName );
         File.WriteAllBytes( calculatedModificationPath, context.Asset.bytes );

         return true;
      }

      protected override bool ReplaceOrUpdateAsset( string calculatedModificationPath, AssetLoadedContext<TextAsset> context )
      {
         var data = File.ReadAllBytes( calculatedModificationPath );
         var text = Encoding.UTF8.GetString( data );
         
         var ext = context.Asset.GetOrCreateExtensionData<TextAssetExtensionData>();
         ext.Data = data;
         ext.Text = text;

         return true;
      }
   }
}