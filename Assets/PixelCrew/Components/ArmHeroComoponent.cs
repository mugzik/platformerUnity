using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ArmHeroComoponent : MonoBehaviour
    {
        private Hero _hero;

        public void ArmHero(GameObject gObject)
        {
            gObject.GetComponent<Hero>()?.ArmHero();
        }
    }
}