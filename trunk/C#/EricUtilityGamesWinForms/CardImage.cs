//***   GameCard Generator   ***//
//***   Produit par : Eric   ***//
//*** ericmas001@hotmail.com ***//


/* Comment l'utiliser ???
 * 
 * Facile, intégrez cette classe dans votre projet.
 * 
 * Copiez l'image "Deck.png" dans le dossier de lancement de 
 *	votre application.
 *
 * Lorsque vous voudrez faire afficher une carte, il suffira d'ajouter 
 *	"using GameCardGenerator;" a votre source.
 * 
 * Ensuite, vous pourrez vous servir des 3 méthodes comme suit dépendant de vos 
 *	besoins. Le résultat sera un Bitmap de 150*[Grosseur] par 200*[Grosseur]
 * 
 * Pour afficher une carte normale (ex: 2 de trefle, Roi de pique, etc.)
 *	CarteGenerator.getCarte( Sorte, Valeur, Grosseur ) où
 *	Sorte: La sorte de la carte (pique,trefle,coeur,carreau)
 *  Valeur: La valeur de la carte (As, Deux, Dame, Roi, etc.)
 *  Grosseur: Le coefficient apporté a la grandeur de base (150x200)
 * 
 *	ex: CarteGenerator.getCarte( SorteCarte.Coeur, ValeurCarte.Dix, 0.5 )
 *	retournera un 10 de coeur de grosseur 75x100
 * 
 * Pour afficher un joker
 *	CarteGenerator.getJoker( Coloré?, Grosseur ) où
 *  Coloré?: Si vous voulez celui en couleur (true,false)
 *  Grosseur: Le coefficient apporté a la grandeur de base (150x200)
 * 
 *	ex: CarteGenerator.getJoker( false, 1 )
 *	retournera un joker, noir et blanc, 150x200
 * 
 * Pour l'envers de la carte
 *	CarteGenerator.getDos( Grosseur ) où
 *  Grosseur: Le coefficient apporté a la grandeur de base (150x200)
 * 
 *	ex: CarteGenerator.getDos( 2 )
 *	retournera une carte retournée, 300x400
 * 
 * Petite source très simple, mais qui pourra sauver du temps a certains
 */

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using EricUtility.Games.CardGame;

namespace EricUtility.Games.Windows.Forms
{
    public static class CardImage
    {// Le chemin pour se rendre a l'image.
        // Le chemin par défaut devrait fonctionner si vous avez suivi les instructions
        // Par défaut: return Application.StartupPath + "\\deck.png";
        public static Image ImageDeck
        {
            get
            {
                return Properties.Resources.deck2;
            }
        }

        // Découpe et retourne la carte sur l'image de base
        // Dimensions: 40x56
        // Ordre Club Heart Diamond Spade / Pokus ColorJoker Muffin DarkJoker
        public static Image GetImage(int x, int y, double size)
        {
            // On va chercher l'image de base
            Image imgSource = ImageDeck;

            // On crée une nouvelle image sur laquel on va dessiner
            Image img = new Bitmap((int)(40 * size), (int)(56 * size));

            // On prends en main notre "crayon" pour dessiner
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Transparent);
            // On note la position et la grosseur de l'image de la carte
            Rectangle rect = new Rectangle(0, 0, (int)(40 * size), (int)(56 * size));

            // On dessine la carte
            g.DrawImage(imgSource, rect, x, y, (int)40, (int)56, GraphicsUnit.Pixel);

            // On rajoute une toute petite bordure autour de la carte
            //g.DrawRectangle(new Pen(Brushes.Black), 0, 0, (int)(41 * size), (int)(57 * size));

            // On renvoi la carte
            return img;
        }

        // Dimensions: 40x56
        // Ordre Club Heart Diamond Spade / Pokus ColorJoker Muffin DarkJoker
        public static Image GetImage(GameCardKind sorte, GameCardValue valeur, double size)
        {
            int v = (int)valeur;
            int k = 0;
            switch (sorte)
            {
                case GameCardKind.Heart: k = 1; break;
                case GameCardKind.Diamond: k = 2; break;
                case GameCardKind.Spade: k = 3; break;
                case GameCardKind.Club: k = 0; break;
            }
            return GetImage(v * 40, k * 56, size);
        }

        public static Image GetImage(GameCardSpecial special, double size)
        {
            if (special == GameCardSpecial.Hidden)
                return GetBackSideImage(size);
            if (special == GameCardSpecial.JokerColor)
                return GetJokerImage(true, size);
            if (special == GameCardSpecial.JokerDark)
                return GetJokerImage(false, size);
            return new Bitmap((int)(40 * size), (int)(56 * size));
        }
        public static Image GetJokerImage(bool colored, double size)
        {
            int offset = (colored ? 0 : 2*56);
            return GetImage(13 * 40, 56 + offset, size);
        }
        public static Image GetBackSideImage(double size)
        {
            return GetImage(13 * 40, 2 * 56, size);
        }
    }
}
