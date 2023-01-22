using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ArmHeroComoponent : MonoBehaviour
    {
        private Creatures.Hero _hero;

        public void ArmHero(GameObject gObject)
        {
            gObject.GetComponent<Creatures.Hero>()?.ArmHero();
        }
    }
}