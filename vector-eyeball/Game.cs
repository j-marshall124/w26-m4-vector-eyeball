// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Vector2[] positions = [new (200, 200), new (100, 100), new (300, 300)];
        float[] radii = [50, 10, 35];

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Eyeball Animation with Vectors");
            Window.SetSize(400, 400);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            for (int i = 0; i < positions.Length; i++)
            {
                Vector2 position = positions[i];
                float radius = radii[i];
                DrawEyeball(position, radius);
            }
        }

        void DrawEyeball(Vector2 eyePosition, float radius)
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
            float magnitude = vectorFromEyeToMouse.Length();

            //Calculate where to position iris and pupil
            Vector2 irisPupilPosition;
            float maxMoveDistance = corneaR - irisR;
            bool isInsideEye = magnitude < maxMoveDistance;
            if (isInsideEye == true)
            {
                irisPupilPosition = mousePosition;
            }
            else // is outside eye
            {
                irisPupilPosition = eyePosition + direction * maxMoveDistance;
            }

            // Iris
            Draw.FillColor = Color.Gray;
            Draw.Circle(irisPupilPosition, irisR);

            // Pupil
            Draw.FillColor = Color.Black;
            Draw.Circle(irisPupilPosition, pupilR);
        }
    }

}
