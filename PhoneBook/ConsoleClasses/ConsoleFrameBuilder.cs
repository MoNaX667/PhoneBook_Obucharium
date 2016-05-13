using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    static class ConsoleFrameBuilder
    {
        /// <summary>
        /// Draw simple frame
        /// </summary>
        /// <param name="startHeight"></param>
        /// <param name="startWidth"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public static void DrawFrame(int startHeight, int startWidth, int height, int width)
        {

            // Top horizontal line
            Console.SetCursorPosition(startWidth, startHeight);
            Console.Write('\u250c');
            Console.Write(new string('\u2500', width - startWidth));
            Console.Write('\u2510');

            // bottom horizontal line
            Console.SetCursorPosition(startWidth, height);
            Console.Write('\u2514');
            Console.Write(new string('\u2500', width - startWidth));
            Console.Write('\u2518');

            // left vertical line
            for (int i = height - 1; i > startHeight; i--)
            {
                Console.SetCursorPosition(startWidth, i);
                Console.Write('\u2502');
            }

            // right vertical line
            for (int i = height - 1; i > startHeight; i--)
            {
                Console.SetCursorPosition(width + 1, i);
                Console.Write('\u2502');
            }
        }

        /// <summary>
        /// It's method draw info frame with even person info like full name and phone number
        /// </summary>
        /// <param name="startHeight"></param>
        /// <param name="startWidth"></param>
        /// <param name="Height"></param>
        /// <param name="width"></param>
        public static void DrawInfoFrame(int startHeight, int startWidth, int height, int width)
        {
            int tempWidth=startWidth;
            // Top horizontal line
            Console.SetCursorPosition(startWidth, startHeight);
            Console.Write('\u250c');
            tempWidth++;
            Console.Write(new string('\u2500', tempWidth+=2));
            Console.Write('\u252c');
            tempWidth++;
            Console.Write(new string('\u2500', tempWidth+=36));
            Console.Write('\u252c');
            tempWidth++;
            Console.Write(new string('\u2500', (width-tempWidth)));
            tempWidth += width - tempWidth;
            Console.Write('\u2510');
            tempWidth++;

            // right vertical line
            for (int i = startHeight +1; i < height; i++)
            {
                Console.SetCursorPosition(tempWidth+6,i);

                // Seporate row char
                if (i == 3)
                {
                    Console.Write('\u2524');
                    continue;
                }

                Console.Write('\u2502');
            }

            // left vertical line
            for (int i = startHeight + 1; i < height; i++)
            {
                Console.SetCursorPosition(startWidth, i);

                // Seporate row char with horizontal line
                if (i == 3)
                {
                    Console.Write('\u251c');
                    Console.Write(new string('\u2500', width+5 ));
                    continue;
                }

                Console.Write('\u2502');
            }

            // bottom horizontal line
            Console.SetCursorPosition(startWidth, height);
            Console.Write('\u2514');
            Console.Write(new string('\u2500',width +5));
            Console.Write('\u2518');

            // Fist Seporate line
            for (int i = startHeight + 1; i <= height; i++){
                Console.SetCursorPosition(startWidth + 5, i);

                if (i == startWidth + 2){
                    Console.Write('\u253c');
                    continue;
                }

                if (i == height){
                    Console.SetCursorPosition(startWidth + 5, i);
                    Console.Write('\u2534');
                    break;
                }

                Console.Write('\u2502');
            }

            // Second Seporate line
            for (int i = startHeight + 1; i <= height; i++){
                Console.SetCursorPosition(startWidth + 47, i);

                if (i == startWidth + 2){
                    Console.Write('\u253c');
                    continue;
                }

                if (i == height){
                    Console.SetCursorPosition(startWidth + 47, i);
                    Console.Write('\u2534');
                    break;
                }

                Console.Write('\u2502');
            }

            // Build title
            Console.SetCursorPosition(2, 2);
            Console.Write("Id".PadLeft(3, ' '));

            Console.SetCursorPosition(21, 2);
            Console.Write("Name of contact");

            Console.SetCursorPosition(52, 2);
            Console.Write("Phone number");
        }

        /// <summary>
        /// It's method draw command frame with list of available command and status window
        /// </summary>
        public static void DrawCommandFrame(int startHeight, int startWidth, int height,
            int width)
        {
            DrawFrame(startHeight, startWidth, height, width);

            // Command block
            Console.SetCursorPosition(startWidth, 3);
            Console.Write('\u251c');
            Console.Write(new string('\u2500', width-startWidth));
            Console.Write('\u2524');

            // Command block
            Console.SetCursorPosition(startWidth, 15);
            Console.Write('\u251c');
            Console.Write(new string('\u2500', width - startWidth));
            Console.Write('\u2524');

            Console.SetCursorPosition(startWidth, 17);
            Console.Write('\u251c');
            Console.Write(new string('\u2500', width - startWidth));
            Console.Write('\u2524');

        }

        /// <summary>
        /// Build terminal frame
        /// </summary>
        /// <param name="startHeight"></param>
        /// <param name="startWidth"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public static void DrawTerminalFrame(int startHeight, int startWidth, int height,
            int width){
            ConsoleFrameBuilder.DrawFrame(startHeight,startWidth,height,width);

            // Set Foregroundcolor for inputing
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            // Welcome tex block
            Console.SetCursorPosition(3,29);
            Console.Write("Are you welcome to control terminal ... Please input command");

            // Set default color
            Console.ForegroundColor = oldColor;
        }
    }
}
