using App.Helpers;

namespace App;

public class SurveyApp
{
    private const string _filePath = "surveys.txt";

    private static void PressEnterToContinue()
    {
        Console.WriteLine("\nPresiona [Enter] para continuar...");
        Console.ReadLine();
    }

    public void Run()
    {
        // Bucle principal de la app
        bool loop = true;
        while (loop)
        {
            int option = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Programa de Encuestas",
                    Choices = ["Agregar nueva respuesta", "Ver todas las respuestas", "Salir"],
                    IsMainMenu = true,
                }
            );

            switch (option)
            {
                case -1:
                case 2:
                {
                    if (HandleExit(option == 2))
                    {
                        loop = false;
                        break;
                    }
                    break;
                }
                case 0:
                {
                    HandleAddSurveyResponse();
                    break;
                }
                case 1:
                {
                    Txt.PrintSurveyTxt(_filePath);
                    PressEnterToContinue();
                    break;
                }
            }
        }
    }

    // Maneja la salida del programa, con confirmación si es necesario
    private static bool HandleExit(bool shouldConfirm)
    {
        if (shouldConfirm)
        {
            var confirm = InteractiveMenu.Show(
                new InteractiveMenu.MenuArgs
                {
                    MenuTitle = "Estas seguro que deseas salir?",
                    Choices = ["Si, deseo salir.", "No, no quiero salir ahora."],
                }
            );

            if (confirm == 0)
            {
                return true;
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    // Maneja la adición de una nueva respuesta de encuesta
    private void HandleAddSurveyResponse()
    {
        Input.ReadRequiredStrArgs strArgs = new Input.ReadRequiredStrArgs { AllowEmpty = false };

        string name = Input.ReadRequiredStr("Ingresa el nombre del encuestado: ", strArgs);
        int? rating;
        do
        {
            rating = Input.ReadRequiredInt(
                "Ingresa una calificación (1-5): ",
                new Input.ReadRequiredIntArgs { AllowEmpty = false }
            );

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("La calificación debe estar entre 1 y 5.");
            }
        } while (rating < 1 || rating > 5);
        string comment = Input.ReadRequiredStr("Ingresa un comentario: ", strArgs);

        Txt.AppendSurveyResponse(_filePath, name, rating ?? 1, comment);

        Console.WriteLine("\nRespuesta agregada exitosamente.");
        PressEnterToContinue();
    }
}
