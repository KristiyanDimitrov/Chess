using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChessLogic.Figures;
using ChessLogic.Figures.Properties;
using static ChessLogic.Figures.Properties.Figure;

namespace ChessUI
{
    static class Images
    {
        private static readonly Dictionary<Type, ImageSource> whiteSources = new()
        {
            {typeof(Pawn),    LoadImage("Assets/PawnW.png") },
            {typeof(Bishop),  LoadImage("Assets/BishopW.png") },
            {typeof(Knight),  LoadImage("Assets/KnightW.png") },
            {typeof(Rook),    LoadImage("Assets/RookW.png") },
            {typeof(Queen),   LoadImage("Assets/QueenW.png") },
            {typeof(King),    LoadImage("Assets/KingW.png") }
        };

        private static readonly Dictionary<Type, ImageSource> blackSources = new()
        {
            {typeof(Pawn),   LoadImage("Assets/PawnB.png") },
            {typeof(Bishop), LoadImage("Assets/BishopB.png") },
            {typeof(Knight), LoadImage("Assets/KnightB.png") },
            {typeof(Rook),   LoadImage("Assets/RookB.png") },
            {typeof(Queen),  LoadImage("Assets/QueenB.png") },
            {typeof(King),   LoadImage("Assets/KingB.png") }
        };

        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(Type figureType, ColorList color)
        {
            return color switch
            {
                ColorList.White => whiteSources[figureType],
                ColorList.Black => blackSources[figureType],
                _ => null
            };
        }

        public static ImageSource GetImage(Figure figure)
        {
            return GetImage(figure.GetType(), figure.color);
        }

    }
}
