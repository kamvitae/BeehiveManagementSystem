using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFIV_6_BeehiveManagementSystem
{
    static class HoneyVault
    {
        const float NECTAR_CONVERSION_RATIO = .19f;
        const float LOW_LEVEL_WARNING = 10f;

        private static float honey = 25f;
        private static float nectar = 100f;

        /// <summary>
        /// Zwraca dane z raportu z ostatniej zmiany przechowywane w polu reprt, w postaci ciągu znaków.
        /// </summary>
        public static string StatusReport
        {
            get
            {
                return  GenerateReportForQueen();
            }
        }

        /// <summary>
        /// Generuje raport zmiany dla królowej i zapisuje go w polu report.
        /// </summary>
        private static string GenerateReportForQueen()
        {
            string status = "";
            status = $"Raport o stanie skarbca:\n" +
                $"Jednostki miodu:{honey}\n" +
                $"Jednostki nektaru:{nectar}\n\n";
            string warnings = "";
            if (LOW_LEVEL_WARNING >= honey)
                warnings += "MAŁO MIODU - DODAJ PRODUCENTÓW MIODU\n";
            if (LOW_LEVEL_WARNING >= nectar)
                warnings += "MAŁO NEKTARU - DODAJ ZBIERACZY NEKTARU\n";
            return status + warnings;
        }

        /// <summary>
        /// Przekształca nektar w miód.
        /// </summary>
        /// <param name="amount">Ilość jednostek nektaru do przekształcenia</param>
        public static void ConvertNectarToHoney(float amount)
        {
            if (amount > nectar)
                amount = nectar;
            nectar -= amount;
            honey += (amount * NECTAR_CONVERSION_RATIO);
        }

        /// <summary>
        /// Sprawdza czy w skarbcu jest wystarczająca ilość miodu do spożycia przez pszczołu dla podtrzymania funkcjonowania ula.
        /// </summary>
        /// <param name="amount">Ilość jednostek miodu do spożycia</param>
        /// <returns>True, jeśli jest wystarczająco dużo miodu do spożycia. W przeciwnym razie false.</returns>
        public static bool ConsumeHoney(float amount)
        {
            if (amount <= honey)
            {
                honey -= amount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gromadzi zasoby nektaru do skarbca.
        /// </summary>
        /// <param name="amount">Ilość jednostek nektaru która ma byc przekazana do skarbca</param>
        public static void CollectNectar(float amount)
        {
            if (amount > 0f) nectar += amount;
        }
    }
}
