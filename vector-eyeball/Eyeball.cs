using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MohawkGame2D
{
    public class Eyeball
    {
        public Vector2 eyePosition;
        public float radius;
        Color color;

        // Constructor
        public Eyeball() 
        {
            // Put it anywhere on screen
            eyePosition = Random.Vector2(Window.Size);
            // Give random size
            radius = Random.Integer(15, 50);
            color = Random.Color();
        }

        public Eyeball(float x, float y, float radius)
        {
            this.eyePosition = new Vector2(x, y);
            this.radius = radius;
        }

        public void DrawEyeball()
        {
            // Calculate ratios for each eye
            float corneaR = radius;
            float irisR = radius * 0.7f; // 0.7 = 70%
            float pupilR = radius * 0.3f; // 0.3 == 30%

            // Cornea
            Draw.LineColor = Color.Black;
            Draw.LineSize = 1;
            Draw.FillColor = Color.White;
            Draw.Circle(eyePosition, corneaR);

            // Set up our look vector
            // To go from point A to point B, we do B - A
            Vector2 mousePosition = Input.GetMousePosition();
            Vector2 vectorFromEyeToMouse = mousePosition - eyePosition;

            // Split vector into it's 2 components: direction and magnitude
            Vector2 direction = Vector2.Normalize(vectorFromEyeToMouse);
            float distance = vectorFromEyeToMouse.Length();

            //Calculate where to position iris and pupil
            Vector2 irisPupilPosition;
            float maxMoveDistance = corneaR - irisR;
            bool isInsideEye = distance < maxMoveDistance;
            if (isInsideEye == true)
            {
                irisPupilPosition = mousePosition;
            }
            else // is outside eye
            {
                irisPupilPosition = eyePosition + direction * maxMoveDistance;
            }

            // Iris
            Draw.FillColor = color;
            Draw.Circle(irisPupilPosition, irisR);

            // Pupil
            Draw.FillColor = Color.Black;
            Draw.Circle(irisPupilPosition, pupilR);
        }
    }
}
