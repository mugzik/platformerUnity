using System.Collections;
using UnityEngine;

using PixelCrew.Creatures.Hero;

namespace PixelCrew.Components.Collectables
{
    public class ArmHeroComoponent : MonoBehaviour
    {
        //private Hero _hero;

        public void ArmHero(GameObject gObject)
        {
            gObject.GetComponent<Hero>()?.ArmHero();
        }
    }
}