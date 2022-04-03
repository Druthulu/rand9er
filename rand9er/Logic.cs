﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

using System.IO;

namespace rand9er
{
    public class Logic
    {

        //   here we design the logic for shuffling
        
        List<int> CityExits = new List<int>()
        {
                262, 300, 311, 312, 350, 450, 455, 503, 550, 602, 650, 701, 706, 707, 750, 800, 806, 850, 851,
                852, 1256, 2950, 2953, 769, 807, 908, 1000, 1300, 1352, 1425, 1450, 1463, 1500, 1555, 1557, 1600,
                1650, 1757, 1950, 1953, 853, 854, 855, 856, 1856, 1908, 2100, 2152, 2164, 2173, 2211, 2212, 2250,
                2261, 2300, 2403, 2450, 2500, 2550, 2750, 2753, 2800, 2850, 2851, 2852, 2853, 2854, 2855, 2856,
                2951, 2952, 2954, 3100, 1700, 2165, 2501, 2751, 2901, 3050
        };  //  these fields have an exit to a world map // 79 
        
        List<int[]> OverWorldConnectors = new List<int[]>()
        {
            new int[] { 262,300,852 },new int[] { 311,350,455,806 },new int[] { 550, 1256 },new int[] { 602,650,701,850,2950 },new int[] { 706 },
            new int[] { 707,750,851 },new int[] { 311,350,455,806 },new int[] { 807,908,1950 },new int[] { 707,750,851,1000 },new int[] { 550, 1256 },
            new int[] { 602,650,701,850,2950 },new int[] { 650,1425,1463,1500 },new int[] { 650,1450,1463,1500 },new int[] { 1557, 1600 },new int[] { 1557, 1600 },
            new int[] { 1555, 1650 },new int[] { 1555, 1650 },new int[] { 1557, 1600 },new int[] { 1557, 1600 },new int[] { 1555, 1757 },new int[] { 1856 },
            new int[] { 807,1908,1950 },new int[] { 650,701,707,750,854,855,1450,1500,1600,2152,2200,2300,2403,2950 },new int[] { 2250 },
            new int[] { 650, 701, 707, 750, 854, 855, 1450, 1500, 1600, 2152, 2300, 2403, 2950 },
            new int[] { 650, 701, 706, 707, 750, 807, 854, 855, 1450, 1500, 1555, 1600, 1757, 1856, 1908, 1950, 2152, 2250, 2300, 2403, 2500, 2800, 2950 },
            new int[] { 650, 701, 706, 707, 750, 807, 854, 855, 1450, 1500, 1555, 1600, 1757, 1856, 1908, 1950, 2152, 2250, 2300, 2403, 2500, 2800, 2950, 2550, 2551, 2553, 2554 },
            new int[] { 650, 701, 706, 707, 750, 807, 854, 855, 1450, 1500, 1555, 1600, 1757, 1856, 1908, 1950, 2152, 2250, 2300, 2403, 2500, 2800, 2950 },
            new int[] { 650, 701, 706, 707, 750, 807, 854, 855, 1450, 1500, 1555, 1600, 1856, 1908, 1950, 2152, 2250, 2300, 2403, 2500, 2800, 2950 },
            new int[] { 650, 701, 706, 707, 750, 807, 854, 855, 1450, 1500, 1555, 1600, 1856, 1908, 1950, 2152, 2250, 2300, 2403, 2500, 2800, 2901, 2950 }
        };  //  these fields connect to each other from the world map side, they change as the story progresses

        public List<int[]> ProgressionComplete = new List<int[]>()
        {
            new int[]{50,1000},
            new int[]{51,1500},
            new int[]{52},
            new int[]{53},
            new int[]{54,1410,1420},
            new int[]{55,1400},
            new int[]{56},
            new int[]{57},
            new int[]{58,1600},
            new int[]{59},
            new int[]{60},
            new int[]{61},
            new int[]{62},
            new int[]{63},
            new int[]{64},
            new int[]{65},
            new int[]{66},
            new int[]{67},
            new int[]{68},
            new int[]{69,1900},
            new int[]{70},
            new int[]{100},
            new int[]{101},
            new int[]{102},
            new int[]{103},
            new int[]{104,1150},
            new int[]{105,1151,1152},
            new int[]{106,1153},
            new int[]{107},
            new int[]{108},
            new int[]{109},
            new int[]{110},
            new int[]{111},
            new int[]{112},
            new int[]{113},
            new int[]{114},
            new int[]{115,1154,1155},
            new int[]{116},
            new int[]{117},
            new int[]{150,1190},
            new int[]{151},
            new int[]{153},
            new int[]{154},
            new int[]{155},
            new int[]{156},
            new int[]{157},
            new int[]{158},
            new int[]{159},
            new int[]{160},
            new int[]{161},
            new int[]{162},
            new int[]{163},
            new int[]{164},
            new int[]{165},
            new int[]{166},
            new int[]{167},
            new int[]{200,2260},
            new int[]{201,2055,2080,2090},
            new int[]{202,2200,2270},
            new int[]{203,2100},
            new int[]{204,2070,2095,2095,2280},
            new int[]{205},
            new int[]{206,2005,2010},
            new int[]{207,2040,2250,2090,2060},
            new int[]{208,2050,2225},
            new int[]{209,1900},
            new int[]{250},
            new int[]{251,2020,2030},
            new int[]{252,2270},
            new int[]{253},
            new int[]{254},
            new int[]{255},
            new int[]{256},
            new int[]{257,2300},
            new int[]{258},
            new int[]{259},
            new int[]{260},
            new int[]{261,2300,2375},
            new int[]{262,2350,2400},
            new int[]{300,2500},
            new int[]{301,2505},
            new int[]{302},
            new int[]{303},
            new int[]{304},
            new int[]{305},
            new int[]{306},
            new int[]{307,2525,2510},
            new int[]{308},
            new int[]{309,2520},
            new int[]{310},
            new int[]{311},
            new int[]{312,2540,2530},
            new int[]{350},
            new int[]{351},
            new int[]{352,2650,2600},
            new int[]{353,2680},
            new int[]{354,2610},
            new int[]{355,2640},
            new int[]{356,2680},
            new int[]{357},
            new int[]{358,2660},
            new int[]{359},
            new int[]{400,2740},
            new int[]{401,2790},
            new int[]{402},
            new int[]{403,2730},
            new int[]{404,2700},
            new int[]{405},
            new int[]{406,2710},
            new int[]{407},
            new int[]{408},
            new int[]{450},
            new int[]{451,2840},
            new int[]{452,2810},
            new int[]{453},
            new int[]{454,2900},
            new int[]{455,2805},
            new int[]{456,2800},
            new int[]{457,2805,2810},
            new int[]{500,2940},
            new int[]{501},
            new int[]{502},
            new int[]{503,2980,2990},
            new int[]{504,2910},
            new int[]{505},
            new int[]{506,2970},
            new int[]{507,2915},
            new int[]{550,3180},
            new int[]{551,3180},
            new int[]{552,3180},
            new int[]{553},
            new int[]{554,3110},
            new int[]{555,3180},
            new int[]{556,3180},
            new int[]{557},
            new int[]{558},
            new int[]{559,3180},
            new int[]{560},
            new int[]{561},
            new int[]{562},
            new int[]{563,3175,3180},
            new int[]{564},
            new int[]{565,3115,3118},
            new int[]{566,3180},
            new int[]{567,3180},
            new int[]{568,3180},
            new int[]{569,3180},
            new int[]{570,3180},
            new int[]{571,3100},
            new int[]{572,3180},
            new int[]{573},
            new int[]{574},
            new int[]{575,3160,3170},
            new int[]{576},
            new int[]{600,3050,3190},
            new int[]{601},
            new int[]{602},
            new int[]{603},
            new int[]{604},
            new int[]{605},
            new int[]{606},
            new int[]{607,3105},
            new int[]{608},
            new int[]{609},
            new int[]{610},
            new int[]{611,3165,3120},
            new int[]{612,3140},
            new int[]{613,3125,3130},
            new int[]{614,3000},
            new int[]{615,3155},
            new int[]{616},
            new int[]{617},
            new int[]{618,3700},
            new int[]{619},
            new int[]{620},
            new int[]{650},
            new int[]{651},
            new int[]{652},
            new int[]{653},
            new int[]{654},
            new int[]{655},
            new int[]{657},
            new int[]{660},
            new int[]{661},
            new int[]{701,3710},
            new int[]{702},
            new int[]{703,3720},
            new int[]{704},
            new int[]{705,3730},
            new int[]{706},
            new int[]{707,3740},
            new int[]{750,3800},
            new int[]{751},
            new int[]{752,3820},
            new int[]{753,3860},
            new int[]{754},
            new int[]{755,3830},
            new int[]{756},
            new int[]{757,3840,3850},
            new int[]{758,3870},
            new int[]{759},
            new int[]{760,3880},
            new int[]{761},
            new int[]{762},
            new int[]{763},
            new int[]{764},
            new int[]{765},
            new int[]{766},
            new int[]{767},
            new int[]{768,3900},
            new int[]{800,3750},
            new int[]{801,3760},
            new int[]{802},
            new int[]{806},
            new int[]{813},
            new int[]{814},
            new int[]{850},
            new int[]{851},
            new int[]{852},
            new int[]{1256},
            new int[]{2950},
            new int[]{2953},
            new int[]{152},
            new int[]{656},
            new int[]{658},
            new int[]{659},
            new int[]{662},
            new int[]{663},
            new int[]{769},
            new int[]{803},
            new int[]{804},
            new int[]{805},
            new int[]{807,4390},
            new int[]{809,4180,4220,4230},
            new int[]{810,4160,4190,4170,4200,4210},
            new int[]{811},
            new int[]{812,4150},
            new int[]{815},
            new int[]{816},
            new int[]{900,4450,4442,4440,4450},
            new int[]{901},
            new int[]{902},
            new int[]{903},
            new int[]{904,4420},
            new int[]{905},
            new int[]{906},
            new int[]{907},
            new int[]{908,4400},
            new int[]{909,4430,4435},
            new int[]{910},
            new int[]{911},
            new int[]{912},
            new int[]{913,4455},
            new int[]{914,4470,4460},
            new int[]{915},
            new int[]{916,4445},
            new int[]{930},
            new int[]{931},
            new int[]{932},
            new int[]{950,4500},
            new int[]{951,4510},
            new int[]{952,4530},
            new int[]{953,4515,4520},
            new int[]{954},
            new int[]{955,5300},
            new int[]{956},
            new int[]{957},
            new int[]{1000,4650},
            new int[]{1001,4660},
            new int[]{1002},
            new int[]{1003},
            new int[]{1004},
            new int[]{1005},
            new int[]{1006},
            new int[]{1007},
            new int[]{1008},
            new int[]{1009},
            new int[]{1010,4900},
            new int[]{1011},
            new int[]{1012},
            new int[]{1013},
            new int[]{1014},
            new int[]{1015},
            new int[]{1016},
            new int[]{1017},
            new int[]{1018},
            new int[]{1050},
            new int[]{1051,4727,4700},
            new int[]{1052,4730},
            new int[]{1053},
            new int[]{1054},
            new int[]{1055,4725},
            new int[]{1056},
            new int[]{1057},
            new int[]{1058},
            new int[]{1059,4720},
            new int[]{1060,4800},
            new int[]{1100},
            new int[]{1101,4910,4888},
            new int[]{1102},
            new int[]{1103},
            new int[]{1104},
            new int[]{1105},
            new int[]{1106},
            new int[]{1107,4860},
            new int[]{1108},
            new int[]{1109},
            new int[]{1110,4980},
            new int[]{1150,5000},
            new int[]{1151},
            new int[]{1152},
            new int[]{1153,4990,5020},
            new int[]{1200},
            new int[]{1201,5200,4600},
            new int[]{1202},
            new int[]{1203,5140},
            new int[]{1204,5160},
            new int[]{1205,5100,5080,5090},
            new int[]{1206,5070,5075},
            new int[]{1207},
            new int[]{1208,5030},
            new int[]{1209},
            new int[]{1210},
            new int[]{1211,5050},
            new int[]{1212},
            new int[]{1213},
            new int[]{1214},
            new int[]{1215},
            new int[]{1216},
            new int[]{1217},
            new int[]{1218},
            new int[]{1219},
            new int[]{1220},
            new int[]{1221},
            new int[]{1222},
            new int[]{1223},
            new int[]{1224},
            new int[]{1225,5120},
            new int[]{1226},
            new int[]{1227},
            new int[]{1250,5400},
            new int[]{1251,5510},
            new int[]{1252},
            new int[]{1253},
            new int[]{1254,5580},
            new int[]{1255,5598,5590},
            new int[]{1300,5600},
            new int[]{1301},
            new int[]{1302},
            new int[]{1303},
            new int[]{1304},
            new int[]{1305},
            new int[]{1306},
            new int[]{1307,5660,5680},
            new int[]{1308},
            new int[]{1309},
            new int[]{1310},
            new int[]{1311},
            new int[]{1312},
            new int[]{1313},
            new int[]{1314},
            new int[]{1315,5670},
            new int[]{1350},
            new int[]{1351},
            new int[]{1352,5690},
            new int[]{1353},
            new int[]{1354},
            new int[]{1355},
            new int[]{1356},
            new int[]{1357},
            new int[]{1358},
            new int[]{1359},
            new int[]{1360},
            new int[]{1361},
            new int[]{1362},
            new int[]{1363},
            new int[]{1364},
            new int[]{1365,3118},
            new int[]{1366},
            new int[]{1367},
            new int[]{1368},
            new int[]{1369},
            new int[]{1370},
            new int[]{1400},
            new int[]{1401},
            new int[]{1402},
            new int[]{1403},
            new int[]{1404},
            new int[]{1405},
            new int[]{1406},
            new int[]{1407},
            new int[]{1408},
            new int[]{1409},
            new int[]{1410},
            new int[]{1411},
            new int[]{1412},
            new int[]{1413},
            new int[]{1414},
            new int[]{1415},
            new int[]{1416},
            new int[]{1417},
            new int[]{1418},
            new int[]{1419},
            new int[]{1420},
            new int[]{1421},
            new int[]{1422,5900},
            new int[]{1423},
            new int[]{1424},
            new int[]{1425,5990},
            new int[]{1450,6212,9410,9450},
            new int[]{1451,6208,6207},
            new int[]{1452,9420},
            new int[]{1453},
            new int[]{1454,6210},
            new int[]{1455},
            new int[]{1456},
            new int[]{1457},
            new int[]{1458,9430},
            new int[]{1459},
            new int[]{1460},
            new int[]{1461},
            new int[]{1462},
            new int[]{1463,6170},
            new int[]{1464},
            new int[]{1500,6240},
            new int[]{1501,6100,6130},
            new int[]{1502},
            new int[]{1503,6250},
            new int[]{1504,6270},
            new int[]{1505,6260,6250,6110},
            new int[]{1506},
            new int[]{1507},
            new int[]{1508},
            new int[]{1509},
            new int[]{1550,6300},
            new int[]{1551},
            new int[]{1552},
            new int[]{1553},
            new int[]{1554,6305},
            new int[]{1555,6310},
            new int[]{1556},
            new int[]{1557},
            new int[]{1600,6600,6622,6800,6880,6890,6690,6600},
            new int[]{1601,6610},
            new int[]{1602,6630,6620,6650},
            new int[]{1603},
            new int[]{1604},
            new int[]{1605,6820,6625,6840},
            new int[]{1606,6640,6870},
            new int[]{1607,6615,6645},
            new int[]{1608,6810,6850},
            new int[]{1609},
            new int[]{1610,6860},
            new int[]{1650,6700},
            new int[]{1651},
            new int[]{1652,6710},
            new int[]{1653},
            new int[]{1654,6930},
            new int[]{1655},
            new int[]{1656},
            new int[]{1657,6920},
            new int[]{1658,6910,6955,6980},
            new int[]{1659,6990},
            new int[]{1660},
            new int[]{1661,6950},
            new int[]{1662,6970},
            new int[]{1663,6960},
            new int[]{1750},
            new int[]{1751},
            new int[]{1752},
            new int[]{1753},
            new int[]{1754,6720},
            new int[]{1755,6730},
            new int[]{1756,6740},
            new int[]{1757,6790,6900,6795,6790},
            new int[]{1758},
            new int[]{1759},
            new int[]{1800,10400},
            new int[]{1950},
            new int[]{1951},
            new int[]{1952},
            new int[]{1953},
            new int[]{808},
            new int[]{853},
            new int[]{854},
            new int[]{855},
            new int[]{856},
            new int[]{1801},
            new int[]{1802},
            new int[]{1803,7060},
            new int[]{1806},
            new int[]{1807,8600,7070},
            new int[]{1808},
            new int[]{1809},
            new int[]{1810},
            new int[]{1811},
            new int[]{1812},
            new int[]{1813},
            new int[]{1814},
            new int[]{1815},
            new int[]{1816,7300},
            new int[]{1817},
            new int[]{1818},
            new int[]{1819,7040},
            new int[]{1820},
            new int[]{1821},
            new int[]{1822},
            new int[]{1823},
            new int[]{1824},
            new int[]{1850},
            new int[]{1851},
            new int[]{1852},
            new int[]{1853},
            new int[]{1854,7020},
            new int[]{1855},
            new int[]{1856},
            new int[]{1857},
            new int[]{1858},
            new int[]{1859},
            new int[]{1860},
            new int[]{1861,7010,7100},
            new int[]{1862},
            new int[]{1863},
            new int[]{1864,7030},
            new int[]{1865},
            new int[]{1866,7200},
            new int[]{1900},
            new int[]{1901},
            new int[]{1902},
            new int[]{1903,7600,7650,7700,7750},
            new int[]{1904},
            new int[]{1905,7550},
            new int[]{1906},
            new int[]{1907},
            new int[]{1908},
            new int[]{1909},
            new int[]{1910},
            new int[]{1911},
            new int[]{1912},
            new int[]{1913},
            new int[]{1914},
            new int[]{1915},
            new int[]{1916},
            new int[]{2000,8500},
            new int[]{2001},
            new int[]{2002},
            new int[]{2003},
            new int[]{2004},
            new int[]{2005},
            new int[]{2006,8710},
            new int[]{2007,8800,8400},
            new int[]{2008},
            new int[]{2009},
            new int[]{2050,8340},
            new int[]{2051},
            new int[]{2052},
            new int[]{2053},
            new int[]{2054,8000},
            new int[]{2055,8800},
            new int[]{2100},
            new int[]{2101},
            new int[]{2102},
            new int[]{2103},
            new int[]{2104},
            new int[]{2105,9000},
            new int[]{2106},
            new int[]{2107,9330},
            new int[]{2108},
            new int[]{2109},
            new int[]{2110},
            new int[]{2111,9330},
            new int[]{2112},
            new int[]{2113},
            new int[]{2114,10030,9330},
            new int[]{2150,9320,10000,9100,9350},
            new int[]{2151},
            new int[]{2152},
            new int[]{2153},
            new int[]{2155},
            new int[]{2157,10050},
            new int[]{2158},
            new int[]{2159},
            new int[]{2160},
            new int[]{2161,9025,9310,9050,10010},
            new int[]{2162},
            new int[]{2163},
            new int[]{2164},
            new int[]{2167},
            new int[]{2168},
            new int[]{2169,9200,9300,9370,10100},
            new int[]{2170},
            new int[]{2171},
            new int[]{2172,9150},
            new int[]{2173},
            new int[]{2200,9510,9530,9800},
            new int[]{2201,9540,9810,9820,9520},
            new int[]{2202,9515,9805},
            new int[]{2203},
            new int[]{2204,9812},
            new int[]{2205,9815},
            new int[]{2206},
            new int[]{2207,9850,9840},
            new int[]{2208},
            new int[]{2209,9550,9845,9860},
            new int[]{2211,9250,9600,9835},
            new int[]{2212,9890},
            new int[]{2213,9825},
            new int[]{2214},
            new int[]{2215},
            new int[]{2216},
            new int[]{2217},
            new int[]{2220},
            new int[]{2221},
            new int[]{2222,9830},
            new int[]{2250,9700},
            new int[]{2251,9705},
            new int[]{2252},
            new int[]{2253,9710,9715},
            new int[]{2254,9720,9725,9730,9735},
            new int[]{2255},
            new int[]{2256},
            new int[]{2257,9740},
            new int[]{2258,9745},
            new int[]{2259,9750},
            new int[]{2260,9760,9790},
            new int[]{2261,9605},
            new int[]{2300},
            new int[]{2301,9950},
            new int[]{2302},
            new int[]{2303},
            new int[]{2304},
            new int[]{2305},
            new int[]{2350},
            new int[]{2351},
            new int[]{2352},
            new int[]{2353},
            new int[]{2354},
            new int[]{2355},
            new int[]{2356},
            new int[]{2357,9990},
            new int[]{2358},
            new int[]{2359},
            new int[]{2360},
            new int[]{2361},
            new int[]{2362},
            new int[]{2363},
            new int[]{2364},
            new int[]{2365},
            new int[]{2400},
            new int[]{2401},
            new int[]{2402},
            new int[]{2403},
            new int[]{2404},
            new int[]{2405},
            new int[]{2406},
            new int[]{2450},
            new int[]{2451},
            new int[]{2452},
            new int[]{2453},
            new int[]{2454},
            new int[]{2455},
            new int[]{2456},
            new int[]{2457},
            new int[]{2458},
            new int[]{2500,10500,10570},
            new int[]{2502,10510,10560,10590},
            new int[]{2503},
            new int[]{2504,10550},
            new int[]{2505,10580,10575},
            new int[]{2506},
            new int[]{2507},
            new int[]{2508},
            new int[]{2509},
            new int[]{2510,10595,10540,10530,10595},
            new int[]{2512,10520},
            new int[]{2513},
            new int[]{2550,10670},
            new int[]{2551,10620,10700},
            new int[]{2552},
            new int[]{2553},
            new int[]{2554},
            new int[]{2600},
            new int[]{2601,10830},
            new int[]{2602},
            new int[]{2603,10840},
            new int[]{2604,10850},
            new int[]{2605,10860},
            new int[]{2606,10880},
            new int[]{2607,10890},
            new int[]{2608},
            new int[]{2650},
            new int[]{2651,10900},
            new int[]{2652,10925},
            new int[]{2653,10920,10902},
            new int[]{2654,10902},
            new int[]{2655},
            new int[]{2656,10915,10905},
            new int[]{2657,10903,10910},
            new int[]{2658,10905},
            new int[]{2659},
            new int[]{2660,10995},
            new int[]{2661},
            new int[]{2700,10930},
            new int[]{2701,10940,10950},
            new int[]{2702},
            new int[]{2703},
            new int[]{2704},
            new int[]{2705},
            new int[]{2706,10960},
            new int[]{2707},
            new int[]{2708},
            new int[]{2709},
            new int[]{2710,10970},
            new int[]{2711,10980},
            new int[]{2712},
            new int[]{2713},
            new int[]{2714},
            new int[]{2715},
            new int[]{2716},
            new int[]{2717,10985},
            new int[]{2718},
            new int[]{2719,10990},
            new int[]{2720},
            new int[]{2750,11100},
            new int[]{2753,11200},
            new int[]{2800,10400},
            new int[]{2801},
            new int[]{2802},
            new int[]{2803},
            new int[]{2850,10400,10600},
            new int[]{2851,10640},
            new int[]{2852,10660},
            new int[]{2853,10700},
            new int[]{2854,10400},
            new int[]{2855,9400},
            new int[]{2856,9400,9910},
            new int[]{2951},
            new int[]{2952},
            new int[]{2954},
            new int[]{2955},
            new int[]{3100,5990},
            new int[]{1700},
            new int[]{1701},
            new int[]{1702},
            new int[]{1703},
            new int[]{1704},
            new int[]{1705},
            new int[]{1706},
            new int[]{1707},
            new int[]{1917},
            new int[]{2154},
            new int[]{2165},
            new int[]{2166},
            new int[]{2501},
            new int[]{2751,11100},
            new int[]{2752},
            new int[]{2754},
            new int[]{2755},
            new int[]{2756,11200},
            new int[]{2900,11610,11615},
            new int[]{2901},
            new int[]{2902},
            new int[]{2903},
            new int[]{2904,11620},
            new int[]{2905,11630},
            new int[]{2906},
            new int[]{2907,11640,11645},
            new int[]{2908,11650,11660},
            new int[]{2909},
            new int[]{2910},
            new int[]{2911},
            new int[]{2912,11670},
            new int[]{2913},
            new int[]{2914,11680},
            new int[]{2915,11690,11700},
            new int[]{2916},
            new int[]{2917,11710},
            new int[]{2918,11720},
            new int[]{2919,11740,11730},
            new int[]{2920,11750},
            new int[]{2921,11760},
            new int[]{2922,11765},
            new int[]{2923},
            new int[]{2924},
            new int[]{2925},
            new int[]{2926,11770},
            new int[]{2927},
            new int[]{2928},
            new int[]{2929},
            new int[]{2930},
            new int[]{2931},
            new int[]{2932},
            new int[]{2933},
            new int[]{2934,12000},
            new int[]{3000,12000},
            new int[]{3001},
            new int[]{3002,12000},
            new int[]{3003,12000},
            new int[]{3004,12000},
            new int[]{3005,12000},
            new int[]{3006,12000},
            new int[]{3007,12000},
            new int[]{3008,12000},
            new int[]{3009,12000},
            new int[]{3010,12000},
            new int[]{3011},
            new int[]{3012,12000},
            new int[]{3050,11100},
            new int[]{3051},
            new int[]{3052},
            new int[]{3053},
            new int[]{3054},
            new int[]{3055},
            new int[]{3056},
            new int[]{3057},
            new int[]{3058},
            new int[]{3059}
        };  //  first element is the fieldID, second and more are story progression markers
        List<int[]> ProgBuilt = new List<int[]>();
        List<int[]> ProgBuild()
        {
            for (int i = 0; i < 12001; i++)
            {
                for (int pc = 0; pc < ProgressionComplete.Count; pc++)
                {
                    if (ProgressionComplete[pc].Length > 1)
                    {
                        for (int pci = 1; pci < ProgressionComplete[pc].Length; pci++)
                        {
                            if (ProgressionComplete[pc][pci] == i)
                                ProgBuilt.Add(new int[] { ProgressionComplete[pc][0], ProgressionComplete[pc][pci]});  //  add fieldID and ScenarioCounter
                        }
                    }
                }
            }
            return ProgBuilt;
        }
        public void LogicOut()
        {
            string temp = ""; string[] temp2 = new string[] { temp, "" };
            int count = 1; string temp4 = ""; int[] temp5 = new int[] { 0, 0, 0, 0, 0, 0, 0 };
            List<int[]> ProgBuilt2 = ProgBuild();

            temp2 = new string[] { temp, "" }; //  using right now
            Directory.SetCurrentDirectory("C:\\Users\\user\\Desktop\\");
            File.WriteAllLines("ProgBuilt.txt", temp2);
            temp = ""; temp2 = new string[] { "", "" };
        }

        //  ProgBuilt now contains list of duplicate fieldIDs in order of Story Progression BUT ITS NOT COMPLETE, see below

        //  need to play through again, and log everytime manual control is taken over by story
        //  compare against progression list, and make sure things like 359->351->352 are known as required
        //  350 world entrance and requirement for dali, is story359 and then after is 350.
        //  so while generating path, we need to know we cannot progress past 352 requirment without actually reaching dali on the overworld.
        //  hmm

    }
}
