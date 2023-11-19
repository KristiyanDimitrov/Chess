using ChessLogic;
using ChessLogic.Figures;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessUI
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<Type> FigureSelected;

        public PromotionMenu(Player player)
        {
            InitializeComponent();

            QueenImg.Source = Images.GetImage(typeof(Queen), player.Color);
            RookImg.Source = Images.GetImage(typeof(Rook), player.Color);
            BishopImg.Source = Images.GetImage(typeof(Bishop), player.Color);
            KnightImg.Source = Images.GetImage(typeof(Knight), player.Color);
        }

        public void DrawPromotion(Image img)
        {
            PromotionGrid.Children.Add(img);
        }

        private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FigureSelected?.Invoke(typeof(Queen));
        }
        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FigureSelected?.Invoke(typeof(Rook));
        }
        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FigureSelected?.Invoke(typeof(Bishop));
        }
        private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FigureSelected?.Invoke(typeof(Knight));
        }


    }
}
