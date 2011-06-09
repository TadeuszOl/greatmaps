﻿
namespace GMap.NET.MapProviders
{
   using System;
   using GMap.NET.Projections;

   public abstract class LatviaMapProviderBase : GMapProvider
   {
      #region GMapProvider Members
      public override Guid Id
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public override string Name
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      readonly LKS92Projection projection = new LKS92Projection();
      public override PureProjection Projection
      {
         get
         {
            return projection;
         }
      }

      GMapProvider[] overlays;
      public override GMapProvider[] Overlays
      {
         get
         {
            if(overlays == null)
            {
               overlays = new GMapProvider[] { this };
            }
            return overlays;
         }
      }

      public override PureImage GetTileImage(GPoint pos, int zoom)
      {
         throw new NotImplementedException();
      }
      #endregion
   }

   /// <summary>
   /// LatviaMap provider, http://www.ikarte.lv/
   /// </summary>
   public class LatviaMapProvider : LatviaMapProviderBase
   {
      public static readonly LatviaMapProvider Instance;

      LatviaMapProvider()
      {
      }

      static LatviaMapProvider()
      {
         Instance = new LatviaMapProvider();
      }

      #region GMapProvider Members

      readonly Guid id = new Guid("2A21CBB1-D37C-458D-905E-05F19536EF1F");
      public override Guid Id
      {
         get
         {
            return id;
         }
      }

      readonly string name = "LatviaMap";
      public override string Name
      {
         get
         {
            return name;
         }
      }

      public override PureImage GetTileImage(GPoint pos, int zoom)
      {
         string url = MakeTileImageUrl(pos, zoom, Language);

         return GetTileImageUsingHttp(url);
      }

      #endregion

      string MakeTileImageUrl(GPoint pos, int zoom, string language)
      {
         // http://www.maps.lt/cache/ikartelv/map/_alllayers/L03/R00000037/C00000053.png

         return string.Format(UrlFormat, zoom, pos.Y, pos.X);
      }

      static readonly string UrlFormat = "http://www.maps.lt/cache/ikartelv/map/_alllayers/L{0:00}/R{1:x8}/C{2:x8}.png";
   }
}