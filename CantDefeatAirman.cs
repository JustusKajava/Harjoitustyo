using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class CantDefeatAirman : PhysicsGame
{
    public override void Begin()
    {
        //Luodaan pelaaja
        PhysicsObject pelaaja = LuoPelaaja(this);

        PhysicsObject AirMan = LuoVihu(this);
        //Tehdään kuuntelija pelaajalle (Nuolinäppäimet)
        LuoKontrollitPelaajalle(this, pelaaja);

        //Luodaan reunat kentälle
        Level.CreateBorders();

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, Exit, "Lopeta peli");
    }

    /// <summary>
    /// Luo pelaajan ja lisää sen parametrina tuotuun peliin.
    /// </summary>
    /// <param name="peli">Peli, johon pelaaja tuodaan</param>
    /// <returns>Pelaajan PhysicsObjectina</returns>
    public static PhysicsObject LuoPelaaja(PhysicsGame peli)
    {
        //Pelaajan säde
        double radius = 50;

        // TODO: Lisää kuva pelaajalle

        //Luodaan pelaaja
        PhysicsObject pelaaja = new PhysicsObject(radius * 2, radius * 2);

        //Pelaajan ulkoasu
        pelaaja.Shape = Shape.Circle;
        pelaaja.Color = Color.Red;

        //Pelaajan pos
        pelaaja.Position = Vector.Zero;
        //Pelaajalle Tag
        pelaaja.Tag = "pelaaja";

        // TODO: Lisää ominaisuudet mm. räjähdykset ei vaikuta ym. ks.(https://trac.cc.jyu.fi/projects/npo/wiki/OliotJaSelitykset#PhysicsObject)
        peli.Add(pelaaja);

        return pelaaja;
    }

    /// <summary>
    /// Luodaan mahdollisuudet ohjata pelaajaa nuolinäppäimillä
    /// </summary>
    /// <param name="peli">Peli, johon pelaaja kuuluu</param>
    /// <param name="pelaaja">Pelaaja jota ohjataan</param>
    public static void LuoKontrollitPelaajalle(PhysicsGame peli, PhysicsObject pelaaja)
    {
        double nopeus = 100;

        // TODO: paremmat liikkuvuudet (Myös vinottain)
        //Arrow Up
        peli.Keyboard.Listen(Key.Up, ButtonState.Pressed, delegate (PhysicsObject obj) { obj.Move(new Vector(0, nopeus) + obj.Velocity); }, "Liikuttaa pelaajaa ylös" , pelaaja);
        peli.Keyboard.Listen(Key.Up, ButtonState.Released, delegate (PhysicsObject obj) { obj.Stop(); }, "", pelaaja);

        //Arrow Down
        peli.Keyboard.Listen(Key.Down, ButtonState.Pressed, delegate (PhysicsObject obj) { obj.Move(new Vector(0, -nopeus) + obj.Velocity); }, "Liikuttaa pelaajaa ylös", pelaaja);
        peli.Keyboard.Listen(Key.Down, ButtonState.Released, delegate (PhysicsObject obj) { obj.Stop(); }, "", pelaaja);
        //Arrow Right
        peli.Keyboard.Listen(Key.Right, ButtonState.Pressed, delegate (PhysicsObject obj) { obj.Move(new Vector(nopeus,0) + obj.Velocity); }, "Liikuttaa pelaajaa ylös", pelaaja);
        peli.Keyboard.Listen(Key.Right, ButtonState.Released, delegate (PhysicsObject obj) { obj.Stop(); }, "", pelaaja);
        //Arrow Left
        peli.Keyboard.Listen(Key.Left, ButtonState.Pressed, delegate (PhysicsObject obj) { obj.Move(new Vector(-nopeus, 0) + obj.Velocity); }, "Liikuttaa pelaajaa ylös", pelaaja);
        peli.Keyboard.Listen(Key.Left, ButtonState.Released, delegate (PhysicsObject obj) { obj.Stop(); }, "", pelaaja);

        //Z:sta ampuu
        peli.Keyboard.Listen(Key.Z, ButtonState.Pressed, AmmuYlos, "Ampuu", peli, pelaaja);
    }

    /// <summary>
    /// Ampuu ylöspäin pelaajan paikasta
    /// </summary>
    /// <param name="peli">Peli, jossa ammutaan</param>
    /// <param name="pelaaja">Lähtökohta</param>
    public static void AmmuYlos(PhysicsGame peli, PhysicsObject pelaaja)
    {
        double koko = 50;
        PhysicsObject ammus = new PhysicsObject(koko, koko);
        ammus.Position = pelaaja.Position;
        ammus.Velocity = new Vector(0, 100);
        ammus.Color = Color.Blue;
        ammus.IgnoresCollisionWith(pelaaja);
        ammus.MaximumLifetime = new TimeSpan(0, 0, 10);
        peli.Add(ammus);
    }

    /// <summary>
    /// Luo vihollisen peliin
    /// </summary>
    /// <param name="peli">Peli johon vihu tulee</param>
    /// <returns>PhysicsObject vihollinen</returns>
    public static PhysicsObject LuoVihu(PhysicsGame peli)
    {
        // TODO: Vihulle hp + hyökkäyksiä
        double koko = 160;
        PhysicsObject vihu = new PhysicsObject(koko, koko);
        vihu.Shape = Shape.Octagon;
        vihu.Tag = "vihu";
        vihu.Position = new Vector(0, 500);
        peli.Add(vihu);
        return vihu;
    }
}
