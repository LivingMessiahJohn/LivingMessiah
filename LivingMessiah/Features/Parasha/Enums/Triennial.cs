﻿// Ignore Spelling: Abrv Deu Tri YomKippur Yom Kippur Teruah BCV

using Ardalis.SmartEnum;
using LivingMessiah.Enums;
using LivingMessiah.Features.Parasha.Toolbar;

namespace LivingMessiah.Features.Parasha.Enums;


public abstract class Triennial : SmartEnum<Triennial>
{
	#region Id's
	private static class Id
	{
		internal const int Gen_01 = 1; // 1.1 | Gen 1:1-2:3
		internal const int Gen_02 = 2; // 1.2 | Gen 2:4-3-24
		internal const int Gen_04 = 3; // 1.3 | Gen 4
		internal const int Gen_05 = 4; // 1.4 | Gen 5:1-6:8
		internal const int Gen_06 = 5; // 2.1 | Gen 6:9-7:24
		internal const int Gen_08a = 6; // 2.2 | Gen 8:1-14
		internal const int Gen_08b = 7; // 2.3 | Gen 8:15-9:17
		internal const int Gen_09 = 8; // 2.4 | Gen 9:18-10:32
		internal const int Gen_11 = 9; // 2.5 | Gen 11
		internal const int Gen_12 = 10; // 3.1 | Gen 12 & 13
		internal const int Gen_14 = 11; // 3.2 | Gen 14
		internal const int Gen_15 = 12; // 3.3 | Gen 15
		internal const int Gen_16 = 13; // 3.4 | Gen 16
		internal const int Gen_17 = 14; // 3.5 | Gen 17
		internal const int Gen_18 = 15; // 4.1 | Gen 18
		internal const int Gen_19 = 16; // 4.2 | Gen 19
		internal const int Gen_20 = 17; // 4.3 | Gen 20
		internal const int Gen_21 = 18; // 4.4 | Gen 21
		internal const int Gen_22 = 19; // 4.5 | Gen 22
		internal const int Gen_23 = 20; // 5.1 | Gen 23
		internal const int Gen_24a = 21; // 5.2 | Gen 24:1-41
		internal const int Gen_24b = 22; // 5.3 | Gen 24:42-67
		internal const int Gen_25a = 23; // 5.4 | Gen 25:1-18
		internal const int Gen_25b = 24; // 6.1 | Gen 25:19-26:11
		internal const int Gen_26 = 25; // 6.2 | Gen 26:12-35
		internal const int Gen_27a = 26; // 6.3 | Gen 27:1-29
		internal const int Gen_27b = 27; // 6.4 | Gen 27:30-28:9
		internal const int Gen_28 = 28; // 7.1 | Gen 28:10-29:30
		internal const int Gen_29 = 29; // 7.2 | Gen 29:31-30:21
		internal const int Gen_30 = 30; // 7.3 | Gen 30:22-31:2
		internal const int Gen_31 = 31; // 7.4 | Gen 31:3-32:3
		internal const int Gen_32 = 32; // 8.1 | Gen 32:4-33:17
		internal const int Gen_33 = 33; // 8.2 | Gen 33:18-35:8
		internal const int Gen_35 = 34; // 8.3 | Gen 35:9-36:43
		internal const int Gen_37 = 35; // 9.1 | Gen 37
		internal const int Gen_38 = 36; // 9.2 | Gen 38
		internal const int Gen_39 = 37; // 9.3 | Gen 39
		internal const int Gen_40 = 38; // 9.4 | Gen 40
		internal const int Gen_41a = 39; // 10.1 | Gen 41:1-37
		internal const int Gen_41b = 40; // 10.2 | Gen 41:38-42:17
		internal const int Gen_42 = 41; // 10.3 | Gen 42:18-43:23
		internal const int Gen_43 = 42; // 10.4 | Gen 43:24-44:17
		internal const int Gen_44 = 43; // 11.1 | Gen 44:18-46:27
		internal const int Gen_46 = 44; // 11.2 | Gen 46:28-47:31
		internal const int Gen_48 = 45; // 12.1 | Gen 48:1-49:27
		internal const int Gen_49 = 46; // 12.2 | Gen 49:28-50:26
		internal const int Exo_01 = 47; // 13.1 | Exo 1 & 2
		internal const int Exo_03 = 48; // 13.2 | Exo 3:1-4:13
		internal const int Exo_04 = 49; // 13.3 | Exo 4:14-6:1
		internal const int Exo_06 = 50; // 14.1 | Exo 6:2-7:7
		internal const int Exo_07 = 51; // 14.2 | Exo 7:8-8:15
		internal const int Exo_08 = 52; // 14.3 | Exo 8:16-9:35
		internal const int Exo_10 = 53; // 15.1 | Exo 10
		internal const int Exo_11 = 54; // 15.2 | Exo 11:1-12:28
		internal const int Exo_12 = 55; // 15.3 | Exo 12:29-51
		internal const int Exo_13a = 56; // 15.4 | Exo 13:1-20
		internal const int Exo_13b = 57; // 16.1 | Exo 13:21-15:21
		internal const int Exo_15 = 58; // 16.2 | Exo 15:22-16:24
		internal const int Exo_16 = 59; // 16.3 | Exo 16:25-17:16
		internal const int Exo_18 = 60; // 17.1 | Exo 18:1-19:6
		internal const int Exo_19 = 61; // 17.2 | Exo 19:7-20:26
		internal const int Exo_21 = 62; // 18.1 | Exo 21:1-22:24
		internal const int Exo_22 = 63; // 18.2 | Exo 22:25-23:33
		internal const int Exo_24 = 64; // 18.3 | Exo 24
		internal const int Exo_25 = 65; // 19.1 | Exo 25:1-26:30
		internal const int Exo_27a = 66; // 19.2 | Exo 26:31-27:19
		internal const int Exo_27b = 67; // 20.1 | Exo 27:20-28:43
		internal const int Exo_29 = 68; // 20.2 | Exo 29
		internal const int Exo_30a = 69; // 20.3 | Exo 30:1-10
		internal const int Exo_30b = 70; // 21.1 | Exo 30:11-38
		internal const int Exo_31 = 71; // 21.2 | Exo 31:1-32:13
		internal const int Exo_32 = 72; // 21.3 | Exo 32:14-34:26
		internal const int Exo_34 = 73; // 22.1 | Exo 34:27-36:38
		internal const int Exo_37 = 74; // 22.2 | Exo 37:1-38:20
		internal const int Exo_38 = 75; // 23.1 | Exo 38:21-31
		internal const int Exo_39 = 76; // 23.2 | Exo 39:1-40:38
		internal const int Lev_01 = 77; // 24.1 | Lev 1 & 2
		internal const int Lev_03 = 78; // 24.2 | Lev 3
		internal const int Lev_04 = 79; // 24.3 | Lev 4
		internal const int Lev_05 = 80; // 24.4 | Lev 5:1-6:7
		internal const int Lev_06 = 81; // 25.1 | Lev 6:8-30
		internal const int Lev_07 = 82; // 25.2 | Lev 7
		internal const int Lev_08 = 83; // 25.3 | Lev 8
		internal const int Lev_09 = 84; // 26.1 | Lev 9 - 11
		internal const int Lev_12 = 85; // 27.1 | Lev 12:1-13:28
		internal const int Lev_13 = 86; // 27.2 | Lev 13:29-59
		internal const int Lev_14 = 87; // 28.1 | Lev 14
		internal const int Lev_15 = 88; // 28.2 | Lev 15
		internal const int Lev_16 = 89; // 30.1 | Lev 16
		internal const int Lev_17 = 90; // 30.2 | Lev 17
		internal const int Lev_18 = 91; // 30.3 | Lev 18
		internal const int Lev_19 = 92; // 31.1 | Lev 19 & 20
		internal const int Lev_21 = 93; // 32.1 | Lev 21
		internal const int Lev_22 = 94; // 32.2 | Lev 22 & 23
		internal const int Lev_24 = 95; // 32.3 | Lev 24
		internal const int Lev_25a = 96; // 33.1 | Lev 25:1-38
		internal const int Lev_25b = 97; // 33.2 | Lev 25:39-26:2
		internal const int Lev_26 = 98; // 34.1 | Lev 26:3-27:34
		internal const int Num_01 = 99; // 35.1 | Num 1:1-2:13
		internal const int Num_02 = 100; // 35.2 | Num 2:14-3:13
		internal const int Num_03 = 101; // 35.3 | Num 3:14-4:20
		internal const int Num_04 = 102; // 37.1 | Num 4:21-5:10
		internal const int Num_05 = 103; // 37.2 | Num 5:11-31
		internal const int Num_06a = 104; // 37.3 | Num 6:1-21
		internal const int Num_06b = 105; // 37.4 | Num 6:22-7:89
		internal const int Num_08 = 106; // 38.1 | Num 8-9
		internal const int Num_10 = 107; // 38.2 | Num 10
		internal const int Num_11 = 108; // 38.3 | Num 11
		internal const int Num_12 = 109; // 38.4 | Num 12
		internal const int Num_13 = 110; // 39.1 | Num 13
		internal const int Num_14 = 111; // 39.2 | Num 14
		internal const int Num_15 = 112; // 39.3 | Num 15
		internal const int Num_16 = 113; // 40.1 | Num 16
		internal const int Num_17 = 114; // 40.2 | Num 17 & 18
		internal const int Num_19 = 115; // 41.1 | Num 19:1-20:13
		internal const int Num_20 = 116; // 41.2 | Num 20:14-22:1
		internal const int Num_22 = 117; // 42.1 | Num 22:2-23:1
		internal const int Num_23 = 118; // 42.2 | Num 23:2-25:9
		internal const int Num_25 = 119; // 43.1 | Num 25:10-26:51
		internal const int Num_26 = 120; // 43.2 | Num 26:52-27:23
		internal const int Num_28 = 121; // 43.3 | Num 28 & 29
		internal const int Num_30 = 122; // 44.1 | Num 30 & 31
		internal const int Num_32 = 123; // 44.2 | Num 32
		internal const int Num_33 = 124; // 45.1 | Num 33
		internal const int Num_34 = 125; // 45.2 | Num 34:1-35:8
		internal const int Num_35 = 126; // 45.3 | Num 35:9-36:13
		internal const int Deu_01 = 127; // 46.1 | Deu 1
		internal const int Deu_02 = 128; // 46.2 | Deu 2:1-3:22
		internal const int Deu_03 = 129; // 47.1 | Deu 3:23-29
		internal const int Deu_04 = 130; // 47.2 | Deu 4
		internal const int Deu_05 = 131; // 47.3 | Deu 5:1-6:3
		internal const int Deu_06 = 132; // 47.4 | Deu 6:4-7:26
		internal const int Deu_08 = 133; // 48.1 | Deu 8
		internal const int Deu_09 = 134; // 48.2 | Deu 9
		internal const int Deu_10 = 135; // 48.3 | Deu 10:1-11:25
		internal const int Deu_11 = 136; // 49.1 | Deu 11:26-12:19
		internal const int Deu_12 = 137; // 49.2 | Deu 12:20-15:6
		internal const int Deu_15 = 138; // 49.3 | Deu 15:7-16:17
		internal const int Deu_16 = 139; // 50.1 | Deu 16:18-17:13
		internal const int Deu_17 = 140; // 50.2 | Deu 17:14-18:13
		internal const int Deu_18 = 141; // 50.3 | Deu 18:14-20:9
		internal const int Deu_20 = 142; // 50.4 | Deu 20:10-21:9
		internal const int Deu_21 = 143; // 51.1 | Deu 21:10-22:5
		internal const int Deu_22 = 144; // 51.2 | Deu 22:6-23:8
		internal const int Deu_23a = 145; // 51.3 | Deu 23:9-20
		internal const int Deu_23b = 146; // 51.4 | Deu 23:21-24:18
		internal const int Deu_24 = 147; // 51.5 | Deu 24:19-25:19
		internal const int Deu_26 = 148; // 52.1 | Deu 26 & 27
		internal const int Deu_28 = 149; // 52.2 | Deu 28:1-29:9
		internal const int Deu_29 = 150; // 53.1 | Deu 29:10-30:10
		internal const int Deu_30 = 151; // 53.2 | Deu 30:11-31:13
		internal const int Deu_31 = 152; // 64.1 | Deu 31:14-30
		internal const int Deu_32 = 153; // 66.1 | Deu 32
		internal const int Deu_33 = 154; // 67.1 | Deu 33
		internal const int Deu_34 = 155; // 68.1 | Deu 34
	}
	#endregion

	#region Declared Public Instances
	public static readonly Triennial Gen_01 = new Gen_01SE(); // 1
	public static readonly Triennial Gen_02 = new Gen_02SE(); // 2
	public static readonly Triennial Gen_04 = new Gen_04SE(); // 3
	public static readonly Triennial Gen_05 = new Gen_05SE(); // 4
	public static readonly Triennial Gen_06 = new Gen_06SE(); // 5
	public static readonly Triennial Gen_08a = new Gen_08aSE(); // 6
	public static readonly Triennial Gen_08b = new Gen_08bSE(); // 7
	public static readonly Triennial Gen_09 = new Gen_09SE(); // 8
	public static readonly Triennial Gen_11 = new Gen_11SE(); // 9
	public static readonly Triennial Gen_12 = new Gen_12SE(); // 10
	public static readonly Triennial Gen_14 = new Gen_14SE(); // 11
	public static readonly Triennial Gen_15 = new Gen_15SE(); // 12
	public static readonly Triennial Gen_16 = new Gen_16SE(); // 13
	public static readonly Triennial Gen_17 = new Gen_17SE(); // 14
	public static readonly Triennial Gen_18 = new Gen_18SE(); // 15
	public static readonly Triennial Gen_19 = new Gen_19SE(); // 16
	public static readonly Triennial Gen_20 = new Gen_20SE(); // 17
	public static readonly Triennial Gen_21 = new Gen_21SE(); // 18
	public static readonly Triennial Gen_22 = new Gen_22SE(); // 19
	public static readonly Triennial Gen_23 = new Gen_23SE(); // 20
	public static readonly Triennial Gen_24a = new Gen_24aSE(); // 21
	public static readonly Triennial Gen_24b = new Gen_24bSE(); // 22
	public static readonly Triennial Gen_25a = new Gen_25aSE(); // 23
	public static readonly Triennial Gen_25b = new Gen_25bSE(); // 24
	public static readonly Triennial Gen_26 = new Gen_26SE(); // 25
	public static readonly Triennial Gen_27a = new Gen_27aSE(); // 26
	public static readonly Triennial Gen_27b = new Gen_27bSE(); // 27
	public static readonly Triennial Gen_28 = new Gen_28SE(); // 28
	public static readonly Triennial Gen_29 = new Gen_29SE(); // 29
	public static readonly Triennial Gen_30 = new Gen_30SE(); // 30
	public static readonly Triennial Gen_31 = new Gen_31SE(); // 31
	public static readonly Triennial Gen_32 = new Gen_32SE(); // 32
	public static readonly Triennial Gen_33 = new Gen_33SE(); // 33
	public static readonly Triennial Gen_35 = new Gen_35SE(); // 34
	public static readonly Triennial Gen_37 = new Gen_37SE(); // 35
	public static readonly Triennial Gen_38 = new Gen_38SE(); // 36
	public static readonly Triennial Gen_39 = new Gen_39SE(); // 37
	public static readonly Triennial Gen_40 = new Gen_40SE(); // 38
	public static readonly Triennial Gen_41a = new Gen_41aSE(); // 39
	public static readonly Triennial Gen_41b = new Gen_41bSE(); // 40
	public static readonly Triennial Gen_42 = new Gen_42SE(); // 41
	public static readonly Triennial Gen_43 = new Gen_43SE(); // 42
	public static readonly Triennial Gen_44 = new Gen_44SE(); // 43
	public static readonly Triennial Gen_46 = new Gen_46SE(); // 44
	public static readonly Triennial Gen_48 = new Gen_48SE(); // 45
	public static readonly Triennial Gen_49 = new Gen_49SE(); // 46
	public static readonly Triennial Exo_01 = new Exo_01SE(); // 47
	public static readonly Triennial Exo_03 = new Exo_03SE(); // 48
	public static readonly Triennial Exo_04 = new Exo_04SE(); // 49
	public static readonly Triennial Exo_06 = new Exo_06SE(); // 50
	public static readonly Triennial Exo_07 = new Exo_07SE(); // 51
	public static readonly Triennial Exo_08 = new Exo_08SE(); // 52
	public static readonly Triennial Exo_10 = new Exo_10SE(); // 53
	public static readonly Triennial Exo_11 = new Exo_11SE(); // 54
	public static readonly Triennial Exo_12 = new Exo_12SE(); // 55
	public static readonly Triennial Exo_13a = new Exo_13aSE(); // 56
	public static readonly Triennial Exo_13b = new Exo_13bSE(); // 57
	public static readonly Triennial Exo_15 = new Exo_15SE(); // 58
	public static readonly Triennial Exo_16 = new Exo_16SE(); // 59
	public static readonly Triennial Exo_18 = new Exo_18SE(); // 60
	public static readonly Triennial Exo_19 = new Exo_19SE(); // 61
	public static readonly Triennial Exo_21 = new Exo_21SE(); // 62
	public static readonly Triennial Exo_22 = new Exo_22SE(); // 63
	public static readonly Triennial Exo_24 = new Exo_24SE(); // 64
	public static readonly Triennial Exo_25 = new Exo_25SE(); // 65
	public static readonly Triennial Exo_27a = new Exo_27aSE(); // 66
	public static readonly Triennial Exo_27b = new Exo_27bSE(); // 67
	public static readonly Triennial Exo_29 = new Exo_29SE(); // 68
	public static readonly Triennial Exo_30a = new Exo_30aSE(); // 69
	public static readonly Triennial Exo_30b = new Exo_30bSE(); // 70
	public static readonly Triennial Exo_31 = new Exo_31SE(); // 71
	public static readonly Triennial Exo_32 = new Exo_32SE(); // 72
	public static readonly Triennial Exo_34 = new Exo_34SE(); // 73
	public static readonly Triennial Exo_37 = new Exo_37SE(); // 74
	public static readonly Triennial Exo_38 = new Exo_38SE(); // 75
	public static readonly Triennial Exo_39 = new Exo_39SE(); // 76
	public static readonly Triennial Lev_01 = new Lev_01SE(); // 77
	public static readonly Triennial Lev_03 = new Lev_03SE(); // 78
	public static readonly Triennial Lev_04 = new Lev_04SE(); // 79
	public static readonly Triennial Lev_05 = new Lev_05SE(); // 80
	public static readonly Triennial Lev_06 = new Lev_06SE(); // 81
	public static readonly Triennial Lev_07 = new Lev_07SE(); // 82
	public static readonly Triennial Lev_08 = new Lev_08SE(); // 83
	public static readonly Triennial Lev_09 = new Lev_09SE(); // 84
	public static readonly Triennial Lev_12 = new Lev_12SE(); // 85
	public static readonly Triennial Lev_13 = new Lev_13SE(); // 86
	public static readonly Triennial Lev_14 = new Lev_14SE(); // 87
	public static readonly Triennial Lev_15 = new Lev_15SE(); // 88
	public static readonly Triennial Lev_16 = new Lev_16SE(); // 89
	public static readonly Triennial Lev_17 = new Lev_17SE(); // 90
	public static readonly Triennial Lev_18 = new Lev_18SE(); // 91
	public static readonly Triennial Lev_19 = new Lev_19SE(); // 92
	public static readonly Triennial Lev_21 = new Lev_21SE(); // 93
	public static readonly Triennial Lev_22 = new Lev_22SE(); // 94
	public static readonly Triennial Lev_24 = new Lev_24SE(); // 95
	public static readonly Triennial Lev_25a = new Lev_25aSE(); // 96
	public static readonly Triennial Lev_25b = new Lev_25bSE(); // 97
	public static readonly Triennial Lev_26 = new Lev_26SE(); // 98
	public static readonly Triennial Num_01 = new Num_01SE(); // 99
	public static readonly Triennial Num_02 = new Num_02SE(); // 100
	public static readonly Triennial Num_03 = new Num_03SE(); // 101
	public static readonly Triennial Num_04 = new Num_04SE(); // 102
	public static readonly Triennial Num_05 = new Num_05SE(); // 103
	public static readonly Triennial Num_06a = new Num_06aSE(); // 104
	public static readonly Triennial Num_06b = new Num_06bSE(); // 105
	public static readonly Triennial Num_08 = new Num_08SE(); // 106
	public static readonly Triennial Num_10 = new Num_10SE(); // 107
	public static readonly Triennial Num_11 = new Num_11SE(); // 108
	public static readonly Triennial Num_12 = new Num_12SE(); // 109
	public static readonly Triennial Num_13 = new Num_13SE(); // 110
	public static readonly Triennial Num_14 = new Num_14SE(); // 111
	public static readonly Triennial Num_15 = new Num_15SE(); // 112
	public static readonly Triennial Num_16 = new Num_16SE(); // 113
	public static readonly Triennial Num_17 = new Num_17SE(); // 114
	public static readonly Triennial Num_19 = new Num_19SE(); // 115
	public static readonly Triennial Num_20 = new Num_20SE(); // 116
	public static readonly Triennial Num_22 = new Num_22SE(); // 117
	public static readonly Triennial Num_23 = new Num_23SE(); // 118
	public static readonly Triennial Num_25 = new Num_25SE(); // 119
	public static readonly Triennial Num_26 = new Num_26SE(); // 120
	public static readonly Triennial Num_28 = new Num_28SE(); // 121
	public static readonly Triennial Num_30 = new Num_30SE(); // 122
	public static readonly Triennial Num_32 = new Num_32SE(); // 123
	public static readonly Triennial Num_33 = new Num_33SE(); // 124
	public static readonly Triennial Num_34 = new Num_34SE(); // 125
	public static readonly Triennial Num_35 = new Num_35SE(); // 126
	public static readonly Triennial Deu_01 = new Deu_01SE(); // 127
	public static readonly Triennial Deu_02 = new Deu_02SE(); // 128
	public static readonly Triennial Deu_03 = new Deu_03SE(); // 129
	public static readonly Triennial Deu_04 = new Deu_04SE(); // 130
	public static readonly Triennial Deu_05 = new Deu_05SE(); // 131
	public static readonly Triennial Deu_06 = new Deu_06SE(); // 132
	public static readonly Triennial Deu_08 = new Deu_08SE(); // 133
	public static readonly Triennial Deu_09 = new Deu_09SE(); // 134
	public static readonly Triennial Deu_10 = new Deu_10SE(); // 135
	public static readonly Triennial Deu_11 = new Deu_11SE(); // 136
	public static readonly Triennial Deu_12 = new Deu_12SE(); // 137
	public static readonly Triennial Deu_15 = new Deu_15SE(); // 138
	public static readonly Triennial Deu_16 = new Deu_16SE(); // 139
	public static readonly Triennial Deu_17 = new Deu_17SE(); // 140
	public static readonly Triennial Deu_18 = new Deu_18SE(); // 141
	public static readonly Triennial Deu_20 = new Deu_20SE(); // 142
	public static readonly Triennial Deu_21 = new Deu_21SE(); // 143
	public static readonly Triennial Deu_22 = new Deu_22SE(); // 144
	public static readonly Triennial Deu_23a = new Deu_23aSE(); // 145
	public static readonly Triennial Deu_23b = new Deu_23bSE(); // 146
	public static readonly Triennial Deu_24 = new Deu_24SE(); // 147
	public static readonly Triennial Deu_26 = new Deu_26SE(); // 148
	public static readonly Triennial Deu_28 = new Deu_28SE(); // 149
	public static readonly Triennial Deu_29 = new Deu_29SE(); // 150
	public static readonly Triennial Deu_30 = new Deu_30SE(); // 151
	public static readonly Triennial Deu_31 = new Deu_31SE(); // 152
	public static readonly Triennial Deu_32 = new Deu_32SE(); // 153
	public static readonly Triennial Deu_33 = new Deu_33SE(); // 154
	public static readonly Triennial Deu_34 = new Deu_34SE(); // 155

	#endregion



	private Triennial(string name, int value) : base(name, value) { } // Constructor


	#region Extra Fields
	public abstract string TriNum { get; } // 1.1	
	public abstract string ParashaName { get; } // B'reisheet (1)	
	public abstract string NameUrl { get; } // b-reisheet-genesis-1-1-to-19-number-1-1	
	public abstract string AhavtaURL { get; } // http://www.ahavta.org/Commentary Y-1/Y1-01.htm	
	public abstract string Meaning { get; } // In the beginning (Days 1 - 4)	

	//Verses
	public abstract VerseRange TorahVerse { get; }
	public abstract List<VerseRange>? HaftorahVerses { get; }
	public abstract List<VerseRange>? BritVerses { get; }

	//Properties

	public bool HasPrevious => this.Value > ParashaFacts.FirstParashaId ? true : false;
	public bool HasNext => this.Value < ParashaFacts.LastParashaId ? true : false;

	public PrevNextVM? PreviousVM
	{
		get
		{
			if (HasPrevious)
			{
				Triennial _prev = Triennial.FromValue(this.Value - 1);
				return new PrevNextVM(_prev, Constants.PrevNextUrl(_prev), "fas fa-arrow-left");
			}
			else
			{
				return null;
			}
		}
	}

	public PrevNextVM? NextVM
	{
		get
		{
			if (HasNext)
			{
				Triennial _next = Triennial.FromValue(this.Value + 1);
				return new PrevNextVM(_next, Constants.PrevNextUrl(_next), "fas fa-arrow-right");
			}
			else
			{
				return null;
			}
		}
	}

	public string Url
	{
		get
		{
			string slug = $"{BibleBook.FromValue(this.TorahVerse.BibleBook).Abrv}_{this.TorahVerse.ChapterVerse.Replace("-", "-to-").Replace(":", "-")}";
			return ($"{Constants.BaseUrl}/{this.Value}/{slug}");
		}
	}

	public DateTime Date
	{
		get
		{
			return Constants.TriennialSeedDate.AddDays(7 * (this.Value - 1));
		}
	}

	public string Title // 1.1, Gen 1:1-19, Sep 29 2018; 
	{
		get
		{
			return $" {this.TriNum}, {BibleBook.FromValue(this.TorahVerse.BibleBook).Abrv} {this.TorahVerse.ChapterVerse}, {this.Date.ToString(DateFormat.YYYY_MM_DD)}";
		}
	}

	public string BCV 
	{
		get
		{
			return $"{BibleBook.FromValue(this.TorahVerse.BibleBook).Title} {this.TorahVerse.ChapterVerse}";
		}
	}

	public TorahBookFilter TorahBookFilter
	{
		get
		{
			return TorahBookFilter.FromValue(this.TorahVerse.BibleBook.Value);
		}
	}

	public string TorahPlusDaysFromOrToShabbat
	{
		get
		{
			return $" {BibleBook.FromValue(this.TorahVerse.BibleBook).Name} {this.TorahVerse.ChapterVerse} {Enums.Constants.DaysFromOrToShabbat(this.Date).ToString()}";
		}
	}

	public string TorahAbrv // Gen 1:1-19
	{
		get
		{
			return $" {BibleBook.FromValue(this.TorahVerse.BibleBook).Abrv} {this.TorahVerse.ChapterVerse}";
		}
	}

	public string Haftorah
	{
		get
		{
			return HaftorahVerses is not null ? String.Join(", ", HaftorahVerses.Select(s => s.BibleBook.Title + " " + s.ChapterVerse)) : ""; // s.BibleBook.Name
		}
	}

	public string HaftorahAbrv
	{
		get
		{
			return HaftorahVerses is not null ? String.Join(", ", HaftorahVerses.Select(s => s.BibleBook.Abrv + " " + s.ChapterVerse)) : "";
		}
	}

	public string Brit
	{
		get
		{
			return BritVerses is not null ? String.Join(", ", BritVerses.Select(s => s.BibleBook.Title + " " + s.ChapterVerse)) : ""; // s.BibleBook.Name
		}
	}

	public string BritAbrv
	{
		get
		{
			return BritVerses is not null ? String.Join(", ", BritVerses.Select(s => s.BibleBook.Abrv + " " + s.ChapterVerse)) : "";
		}
	}



	#endregion

	#region Private Instantiation

	private sealed class Gen_01SE : Triennial
	{
		public Gen_01SE() : base($"{nameof(Id.Gen_01)}", Id.Gen_01) { }
		public override string TriNum => "1.1";
		public override string ParashaName => "B'reisheet";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-01.htm";
		public override string Meaning => "In the beginning";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "1:1-2:3", 1, 34);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Job, "38:11-40:2", 13805, 13867),
new VerseRange(BibleBook.Isaiah, "42:5-7", 18486, 18488),
new VerseRange(BibleBook.Isaiah, "45:17-19", 18579, 18581),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "1:1-5", 26046, 26050),
new VerseRange(BibleBook.Colossians, "1:1-23", 29467, 29489),
new VerseRange(BibleBook.Revelation, "22:6-21", 31087, 31102),];

	}

	private sealed class Gen_02SE : Triennial
	{
		public Gen_02SE() : base($"{nameof(Id.Gen_02)}", Id.Gen_02) { }
		public override string TriNum => "1.2";
		public override string ParashaName => "Elleh toldot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-02.htm";
		public override string Meaning => "These are the Generations";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "2:4-3-24", 35, 80);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "28:11-26", 21169, 21184),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "1:6-28", 26051, 26073),
new VerseRange(BibleBook.Romans, "5:12-21", 28060, 28069),
new VerseRange(BibleBook.Hebrews, "11:1-7", 30174, 30180),];

	}

	private sealed class Gen_04SE : Triennial
	{
		public Gen_04SE() : base($"{nameof(Id.Gen_04)}", Id.Gen_04) { }
		public override string TriNum => "1.3";
		public override string ParashaName => "Adam Yada Hava";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-03.htm";
		public override string Meaning => "Adam knew Eve";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "4", 81, 106);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "42:7-8", 18488, 18489),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "2:39-41", 25013, 25015),
new VerseRange(BibleBook.Romans, "3:1-24", 27993, 28016),];

	}

	private sealed class Gen_05SE : Triennial
	{
		public Gen_05SE() : base($"{nameof(Id.Gen_05)}", Id.Gen_05) { }
		public override string TriNum => "1.4";
		public override string ParashaName => "Tol'dot Adahm";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-04.htm";
		public override string Meaning => "Generations of Adam";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "5:1-6:8", 107, 146);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "30:8-15", 18226, 18233),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "1:11-23", 26056, 26068),];

	}

	private sealed class Gen_06SE : Triennial
	{
		public Gen_06SE() : base($"{nameof(Id.Gen_06)}", Id.Gen_06) { }
		public override string TriNum => "2.1";
		public override string ParashaName => "Noach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-05.htm";
		public override string Meaning => "Rest";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "6:9-7:24", 147, 184);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "54:5-8", 18729, 18732),
new VerseRange(BibleBook.Ezekiel, "14:14", 20746, 20746),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "24:4-14", 23962, 23972),
new VerseRange(BibleBook.John, "1:24-34", 26069, 26079),];

	}

	private sealed class Gen_08aSE : Triennial
	{
		public Gen_08aSE() : base($"{nameof(Id.Gen_08a)}", Id.Gen_08a) { }
		public override string TriNum => "2.2";
		public override string ParashaName => "Vayizkor";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-06.htm";
		public override string Meaning => "And He remebered";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "8:1-14", 185, 198);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Habakkuk, "3:1-5", 22770, 22774),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "1:1-14", 26925, 26938),
new VerseRange(BibleBook.FirstPeter, "3:20", 30445, 30445),
new VerseRange(BibleBook.Revelation, "1:9-20", 30707, 30718),];

	}

	private sealed class Gen_08bSE : Triennial
	{
		public Gen_08bSE() : base($"{nameof(Id.Gen_08b)}", Id.Gen_08b) { }
		public override string TriNum => "2.3";
		public override string ParashaName => "Tzay meen hatayvah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-07.htm";
		public override string Meaning => "Come out from the ark";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "8:15-9:17", 199, 223);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "42:9-21", 18490, 18502),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "24:4-14", 23962, 23972),
new VerseRange(BibleBook.SecondTimothy, "2:8-19", 29836, 29847),];

	}

	private sealed class Gen_09SE : Triennial
	{
		public Gen_09SE() : base($"{nameof(Id.Gen_09)}", Id.Gen_09) { }
		public override string TriNum => "2.4";
		public override string ParashaName => "Venay Noach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-09.htm";
		public override string Meaning => "Sons of Noah";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "9:18-10:32", 224, 267);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "49:9-13", 18646, 18650),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "17:26-28", 27550, 27552),
new VerseRange(BibleBook.Revelation, "5", 30781, 30794),];

	}

	private sealed class Gen_11SE : Triennial
	{
		public Gen_11SE() : base($"{nameof(Id.Gen_11)}", Id.Gen_11) { }
		public override string TriNum => "2.5";
		public override string ParashaName => "Vayehee kol haaretz";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-10.htm";
		public override string Meaning => "And he had all the land";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "11", 268, 299);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "28:10-16", 18175, 18181),
new VerseRange(BibleBook.Zephaniah, "3:9", 22830, 22830),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "14:20-33", 28699, 28712),];

	}

	private sealed class Gen_12SE : Triennial
	{
		public Gen_12SE() : base($"{nameof(Id.Gen_12)}", Id.Gen_12) { }
		public override string TriNum => "3.1";
		public override string ParashaName => "Lech L'cha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-11.htm";
		public override string Meaning => "Get yourself out";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "12 & 13", 300, 337);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "24:3-18", 6480, 6495),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Galatians, "2:20", 29102, 29102),
new VerseRange(BibleBook.SecondTimothy, "2:11", 29839, 29839),
new VerseRange(BibleBook.Hebrews, "11:1-10", 30174, 30183),];

	}

	private sealed class Gen_14SE : Triennial
	{
		public Gen_14SE() : base($"{nameof(Id.Gen_14)}", Id.Gen_14) { }
		public override string TriNum => "3.2";
		public override string ParashaName => "B'may Amraphel";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-12.htm";
		public override string Meaning => "Days of Amraphel";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "14", 338, 361);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "10:9", 9089, 9089),
new VerseRange(BibleBook.Isaiah, "41:2-14", 18454, 18466),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "7:1-19", 30066, 30084),];

	}

	private sealed class Gen_15SE : Triennial
	{
		public Gen_15SE() : base($"{nameof(Id.Gen_15)}", Id.Gen_15) { }
		public override string TriNum => "3.3";
		public override string ParashaName => "Bahmahchazeh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-13.htm";
		public override string Meaning => "In a vision";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "15", 362, 382);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "1:1-27", 17656, 17682),
new VerseRange(BibleBook.Zephaniah, "3:9-19", 22830, 22840),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "4:1-9", 28024, 28032),];

	}

	private sealed class Gen_16SE : Triennial
	{
		public Gen_16SE() : base($"{nameof(Id.Gen_16)}", Id.Gen_16) { }
		public override string TriNum => "3.4";
		public override string ParashaName => "Sarai ayshet Avram";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-14.htm";
		public override string Meaning => "Sarai wife of Avram";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "16", 383, 398);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "54:1", 18725, 18725),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Galatians, "4:21-31", 29153, 29163),];

	}

	private sealed class Gen_17SE : Triennial
	{
		public Gen_17SE() : base($"{nameof(Id.Gen_17)}", Id.Gen_17) { }
		public override string TriNum => "3.5";
		public override string ParashaName => "Vayhee Avram";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-15.htm";
		public override string Meaning => "When Avram";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "17", 399, 425);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "63:10-19", 18877, 18886),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "4:10-25", 28033, 28048),];

	}

	private sealed class Gen_18SE : Triennial
	{
		public Gen_18SE() : base($"{nameof(Id.Gen_18)}", Id.Gen_18) { }
		public override string TriNum => "4.1";
		public override string ParashaName => "Vayera";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-16.htm";
		public override string Meaning => "And He appeared";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "18", 426, 458);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondKings, "4:1", 9605, 9605),
new VerseRange(BibleBook.Isaiah, "33:17-24", 18297, 18304),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "8:40-56", 25286, 25302),
new VerseRange(BibleBook.Hebrews, "11:8-18", 30181, 30191),];

	}

	private sealed class Gen_19SE : Triennial
	{
		public Gen_19SE() : base($"{nameof(Id.Gen_19)}", Id.Gen_19) { }
		public override string TriNum => "4.2";
		public override string ParashaName => "Vayavo'u sh'ne";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-17.htm";
		public override string Meaning => "And came the two";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "19", 459, 496);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "17:14-18:7", 17998, 18005),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "17:20-37", 25672, 25689),];

	}

	private sealed class Gen_20SE : Triennial
	{
		public Gen_20SE() : base($"{nameof(Id.Gen_20)}", Id.Gen_20) { }
		public override string TriNum => "4.3";
		public override string ParashaName => "VaYisa'a Misham";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-18.htm";
		public override string Meaning => "And journeyed from ..";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "20", 497, 514);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "61:9-10", 18853, 18854),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "8:1-13", 23347, 23359),
new VerseRange(BibleBook.FirstCorinthians, "7:1-2", 28489, 28490),
new VerseRange(BibleBook.Galatians, "3:15-29", 29118, 29132),];

	}

	private sealed class Gen_21SE : Triennial
	{
		public Gen_21SE() : base($"{nameof(Id.Gen_21)}", Id.Gen_21) { }
		public override string TriNum => "4.4";
		public override string ParashaName => "VaHa-Shem Paqad";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-19.htm";
		public override string Meaning => "And YHVH visited";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "21", 515, 548);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "2:21-28", 7262, 7269),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "1:18-25", 23163, 23170),
new VerseRange(BibleBook.Hebrews, "11:11", 30184, 30184),];

	}

	private sealed class Gen_22SE : Triennial
	{
		public Gen_22SE() : base($"{nameof(Id.Gen_22)}", Id.Gen_22) { }
		public override string TriNum => "4.5";
		public override string ParashaName => "V'HaElohim Nisah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-20.htm";
		public override string Meaning => "And Elohim tested";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "22", 549, 572);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "33:7-22", 18287, 18302),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "27:27-66", 24157, 24196),
new VerseRange(BibleBook.John, "19:16-17", 26842, 26843),];

	}

	private sealed class Gen_23SE : Triennial
	{
		public Gen_23SE() : base($"{nameof(Id.Gen_23)}", Id.Gen_23) { }
		public override string TriNum => "5.1";
		public override string ParashaName => "Chaye Sarah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-21.htm";
		public override string Meaning => "Life of Sarah";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "23", 573, 592);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "1:1-31", 8719, 8749),
new VerseRange(BibleBook.Isaiah, "1:1-27", 17656, 17682),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "19:41", 26867, 26867),
new VerseRange(BibleBook.Acts, "7:1-18", 27118, 27135),
new VerseRange(BibleBook.FirstCorinthians, "15:50-57", 28769, 28776),];

	}

	private sealed class Gen_24aSE : Triennial
	{
		public Gen_24aSE() : base($"{nameof(Id.Gen_24a)}", Id.Gen_24a) { }
		public override string TriNum => "5.2";
		public override string ParashaName => "V'Avraham Zaken";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-22.htm";
		public override string Meaning => "And Abraham was old";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "24:1-41", 593, 633);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Judges, "19:20-21", 7045, 7046),
new VerseRange(BibleBook.Isaiah, "40:1-2", 18422, 18423),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "4:7-15", 26164, 26172),
new VerseRange(BibleBook.Ephesians, "5:15-33", 29320, 29338),];

	}

	private sealed class Gen_24bSE : Triennial
	{
		public Gen_24bSE() : base($"{nameof(Id.Gen_24b)}", Id.Gen_24b) { }
		public override string TriNum => "5.3";
		public override string ParashaName => "Va'Avo HaYom";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-23.htm";
		public override string Meaning => "I came today";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "24:42-67", 634, 659);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "49:14-51:3", 18651, 18677),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.James, "4:13-17", 30351, 30355),];

	}

	private sealed class Gen_25aSE : Triennial
	{
		public Gen_25aSE() : base($"{nameof(Id.Gen_25a)}", Id.Gen_25a) { }
		public override string TriNum => "5.4";
		public override string ParashaName => "VaYosef Avraham";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "";
		public override string Meaning => "And added Avraham";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "25:1-18", 660, 677);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondSamuel, "5:17-6:1", 8150, 8159),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "7", 28093, 28117),];

	}

	private sealed class Gen_25bSE : Triennial
	{
		public Gen_25bSE() : base($"{nameof(Id.Gen_25b)}", Id.Gen_25b) { }
		public override string TriNum => "6.1";
		public override string ParashaName => "Tole'dot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-24.htm";
		public override string Meaning => "Generations";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "25:19-26:11", 678, 704);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "20:18", 7749, 7749),
new VerseRange(BibleBook.FirstSamuel, "20:42", 7773, 7773),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "9:9-14", 28165, 28170),
new VerseRange(BibleBook.Hebrews, "12:14-29", 30227, 30242),];

	}

	private sealed class Gen_26SE : Triennial
	{
		public Gen_26SE() : base($"{nameof(Id.Gen_26)}", Id.Gen_26) { }
		public override string TriNum => "6.2";
		public override string ParashaName => "Vauiz'ra Yitschaq";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-25.htm";
		public override string Meaning => "And Isaac sowed";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "26:12-35", 705, 728);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "51:12-52:12", 18686, 18709),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "16:1-15", 25622, 25636),
new VerseRange(BibleBook.Romans, "9:1-8", 28157, 28164),];

	}

	private sealed class Gen_27aSE : Triennial
	{
		public Gen_27aSE() : base($"{nameof(Id.Gen_27a)}", Id.Gen_27a) { }
		public override string TriNum => "6.3";
		public override string ParashaName => "Vay'hi Ki-Zeqen";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-26.htm";
		public override string Meaning => "And it was when old";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "27:1-29", 729, 757);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "46:3-6", 18590, 18593),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "2:14-18", 29992, 29996),
new VerseRange(BibleBook.Hebrews, "11:20", 30193, 30193),];

	}

	private sealed class Gen_27bSE : Triennial
	{
		public Gen_27bSE() : base($"{nameof(Id.Gen_27b)}", Id.Gen_27b) { }
		public override string TriNum => "6.4";
		public override string ParashaName => "V'yitem L'kha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-27.htm";
		public override string Meaning => "And may give you";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "27:30-28:9", 758, 783);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Micah, "1:11", 22591, 22591),
new VerseRange(BibleBook.Micah, "5", 22635, 22649),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "10:20-11:36", 28209, 28246),];

	}

	private sealed class Gen_28SE : Triennial
	{
		public Gen_28SE() : base($"{nameof(Id.Gen_28)}", Id.Gen_28) { }
		public override string TriNum => "7.1";
		public override string ParashaName => "Vayetse";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-28.htm";
		public override string Meaning => "And He went out";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "28:10-29:30", 784, 826);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "60", 18823, 18844),
new VerseRange(BibleBook.Hosea, "12:13", 22266, 22266),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "9:18-19", 23398, 23399),
new VerseRange(BibleBook.Luke, "2:1-31", 24975, 25005),
new VerseRange(BibleBook.Ephesians, "6:5-9", 29343, 29347),];

	}

	private sealed class Gen_29SE : Triennial
	{
		public Gen_29SE() : base($"{nameof(Id.Gen_29)}", Id.Gen_29) { }
		public override string TriNum => "7.2";
		public override string ParashaName => "Vayara YHVH";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-29.htm";
		public override string Meaning => "YHVH Saw";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "29:31-30:21", 827, 852);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "60:15", 18837, 18837),
new VerseRange(BibleBook.Isaiah, "61:10-63:9", 18854, 18876),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "11:25-36", 28235, 28246),
new VerseRange(BibleBook.Revelation, "19", 31019, 31039),];

	}

	private sealed class Gen_30SE : Triennial
	{
		public Gen_30SE() : base($"{nameof(Id.Gen_30)}", Id.Gen_30) { }
		public override string TriNum => "7.3";
		public override string ParashaName => "Vayizchar Elohim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-30.htm";
		public override string Meaning => "Elohim Remebered";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "30:22-31:2", 853, 876);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "1:11", 7224, 7224),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "2:13", 23183, 23183),
new VerseRange(BibleBook.Acts, "13:16-41", 27379, 27404),
new VerseRange(BibleBook.Revelation, "20:4-15", 31043, 31054),];

	}

	private sealed class Gen_31SE : Triennial
	{
		public Gen_31SE() : base($"{nameof(Id.Gen_31)}", Id.Gen_31) { }
		public override string TriNum => "7.4";
		public override string ParashaName => "Shuv El Eretz";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-31.htm";
		public override string Meaning => "Return to the Land";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "31:3-32:3", 877, 932);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "30:10-16", 19678, 19684),
new VerseRange(BibleBook.Micah, "6:3-7:20", 22652, 22685),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "1:24-28", 26069, 26073),
new VerseRange(BibleBook.James, "4:1-12", 30339, 30350),];

	}

	private sealed class Gen_32SE : Triennial
	{
		public Gen_32SE() : base($"{nameof(Id.Gen_32)}", Id.Gen_32) { }
		public override string TriNum => "8.1";
		public override string ParashaName => "Vayishlach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-32.htm";
		public override string Meaning => "And He sent";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "32:4-33:17", 933, 978);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Obadiah, "1:1", 22512, 22512),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.James, "1:1-12", 30268, 30279),];

	}

	private sealed class Gen_33SE : Triennial
	{
		public Gen_33SE() : base($"{nameof(Id.Gen_33)}", Id.Gen_33) { }
		public override string TriNum => "8.2";
		public override string ParashaName => "Vayavo";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-33.htm";
		public override string Meaning => "Then He Arrived";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "33:18-35:8", 979, 1020);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Nahum, "1:12-2:5", 22697, 22705),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "22:8-21", 31089, 31102),];

	}

	private sealed class Gen_35SE : Triennial
	{
		public Gen_35SE() : base($"{nameof(Id.Gen_35)}", Id.Gen_35) { }
		public override string TriNum => "8.3";
		public override string ParashaName => "Vayrah Elohim el";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-34.htm";
		public override string Meaning => "Then Elohim appeared";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "35:9-36:43", 1021, 1084);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "43:1-7", 18507, 18513),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "5:1-12", 23236, 23247),];

	}

	private sealed class Gen_37SE : Triennial
	{
		public Gen_37SE() : base($"{nameof(Id.Gen_37)}", Id.Gen_37) { }
		public override string TriNum => "9.1";
		public override string ParashaName => "Vayeshev";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-35.htm";
		public override string Meaning => "And He dwelt";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "37", 1085, 1120);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "38:8", 19904, 19904),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "19:1-20:10", 26827, 26878),];

	}

	private sealed class Gen_38SE : Triennial
	{
		public Gen_38SE() : base($"{nameof(Id.Gen_38)}", Id.Gen_38) { }
		public override string TriNum => "9.2";
		public override string ParashaName => "VaYered Yehudah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-36.htm";
		public override string Meaning => "Judah Departed";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "38", 1121, 1150);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "37:31-37", 18384, 18390),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "9:22-29", 28178, 28185),];

	}

	private sealed class Gen_39SE : Triennial
	{
		public Gen_39SE() : base($"{nameof(Id.Gen_39)}", Id.Gen_39) { }
		public override string TriNum => "9.3";
		public override string ParashaName => "VaYosheph";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-37.htm";
		public override string Meaning => "Now Joseph";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "39", 1151, 1173);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "52:3-9", 18700, 18706),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "7:9-37", 27126, 27154),];

	}

	private sealed class Gen_40SE : Triennial
	{
		public Gen_40SE() : base($"{nameof(Id.Gen_40)}", Id.Gen_40) { }
		public override string TriNum => "9.4";
		public override string ParashaName => "Chateu";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-38.htm";
		public override string Meaning => "They offended";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "40", 1174, 1196);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Amos, "1:3-15", 22368, 22380),
new VerseRange(BibleBook.Amos, "2:6", 22386, 22386),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.SecondCorinthians, "9:6-15", 28963, 28972),];

	}

	private sealed class Gen_41aSE : Triennial
	{
		public Gen_41aSE() : base($"{nameof(Id.Gen_41a)}", Id.Gen_41a) { }
		public override string TriNum => "10.1";
		public override string ParashaName => "Miketz";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-39.htm";
		public override string Meaning => "At the end of";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "41:1-37", 1197, 1233);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "3:15-4:1", 8832, 8846),
new VerseRange(BibleBook.Isaiah, "29:8", 18202, 18202),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "2", 23171, 23193),];

	}

	private sealed class Gen_41bSE : Triennial
	{
		public Gen_41bSE() : base($"{nameof(Id.Gen_41b)}", Id.Gen_41b) { }
		public override string TriNum => "10.2";
		public override string ParashaName => "Hanimtza";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-40.htm";
		public override string Meaning => "Can We Find?";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "41:38-42:17", 1234, 1270);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "11:2-9", 17887, 17894),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "7:1-19", 27118, 27136),];

	}

	private sealed class Gen_42SE : Triennial
	{
		public Gen_42SE() : base($"{nameof(Id.Gen_42)}", Id.Gen_42) { }
		public override string TriNum => "10.3";
		public override string ParashaName => "Vayomer Eleichem";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-41.htm";
		public override string Meaning => "He Said to Them";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "42:18-43:23", 1271, 1314);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "50:10-52:11", 18673, 18708),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "1:68-79", 24962, 24973),];

	}

	private sealed class Gen_43SE : Triennial
	{
		public Gen_43SE() : base($"{nameof(Id.Gen_43)}", Id.Gen_43) { }
		public override string TriNum => "10.4";
		public override string ParashaName => "Vayavei Ha-ish";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-42.htm";
		public override string Meaning => "Then the Man Brought";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "43:24-44:17", 1315, 1342);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "42:12-17", 19988, 19993),
new VerseRange(BibleBook.Jeremiah, "43:12-13", 20010, 20011),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "19:37-48", 25769, 25780),];

	}

	private sealed class Gen_44SE : Triennial
	{
		public Gen_44SE() : base($"{nameof(Id.Gen_44)}", Id.Gen_44) { }
		public override string TriNum => "11.1";
		public override string ParashaName => "Vayigash";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-43.htm";
		public override string Meaning => "And He drew near";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "44:18-46:27", 1343, 1414);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "14:6", 6194, 6194),
new VerseRange(BibleBook.Ezekiel, "37:10", 21408, 21408),
new VerseRange(BibleBook.Ezekiel, "37:15-18", 21413, 21416),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "2:23", 26973, 26973),
new VerseRange(BibleBook.Acts, "2:36", 26986, 26986),
new VerseRange(BibleBook.Revelation, "12:1-6", 30893, 30898),];

	}

	private sealed class Gen_46SE : Triennial
	{
		public Gen_46SE() : base($"{nameof(Id.Gen_46)}", Id.Gen_46) { }
		public override string TriNum => "11.2";
		public override string ParashaName => "Yehudah Shalach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-44a.htm";
		public override string Meaning => "He Sent Judah";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "46:28-47:31", 1415, 1452);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "2:1-4", 8772, 8775),
new VerseRange(BibleBook.SecondKings, "13:14", 9886, 9886),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "21:1-10", 31055, 31064),
new VerseRange(BibleBook.Revelation, "22:1-10", 31082, 31091),];

	}

	private sealed class Gen_48SE : Triennial
	{
		public Gen_48SE() : base($"{nameof(Id.Gen_48)}", Id.Gen_48) { }
		public override string TriNum => "12.1";
		public override string ParashaName => "Vay'chi";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-44b.htm";
		public override string Meaning => "And He lived";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "48:1-49:27", 1453, 1501);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "43:2", 18508, 18508),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "19:39-20:9", 26865, 26877),];

	}

	private sealed class Gen_49SE : Triennial
	{
		public Gen_49SE() : base($"{nameof(Id.Gen_49)}", Id.Gen_49) { }
		public override string TriNum => "12.2";
		public override string ParashaName => "Shivtey Yisrael";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-45.htm";
		public override string Meaning => "The Tribes of Israel";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Genesis, "49:28-50:26", 1502, 1533);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Micah, "2:12", 22608, 22608),
new VerseRange(BibleBook.Zechariah, "14:1", 23070, 23070),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "21:12-19", 26911, 26918),];

	}

	private sealed class Exo_01SE : Triennial
	{
		public Exo_01SE() : base($"{nameof(Id.Exo_01)}", Id.Exo_01) { }
		public override string TriNum => "13.1";
		public override string ParashaName => "Shemot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-46.htm";
		public override string Meaning => "Names";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "1 & 2", 1534, 1580);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "27:6", 18158, 18158),
new VerseRange(BibleBook.Isaiah, "52:1-6", 18698, 18703),
new VerseRange(BibleBook.Isaiah, "65:19-23", 18917, 18921),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "7:17-29", 27134, 27146),
new VerseRange(BibleBook.SecondCorinthians, "6:1-10", 28900, 28909),];

	}

	private sealed class Exo_03SE : Triennial
	{
		public Exo_03SE() : base($"{nameof(Id.Exo_03)}", Id.Exo_03) { }
		public override string TriNum => "13.2";
		public override string ParashaName => "Umoshe";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-47.htm";
		public override string Meaning => "Now Moses";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "3:1-4:13", 1581, 1615);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondKings, "20:8", 10107, 10107),
new VerseRange(BibleBook.Isaiah, "40:11", 18432, 18432),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "12:26", 23516, 23516),
new VerseRange(BibleBook.Luke, "20:37", 25817, 25817),
new VerseRange(BibleBook.Acts, "7:30", 27147, 27147),];

	}

	private sealed class Exo_04SE : Triennial
	{
		public Exo_04SE() : base($"{nameof(Id.Exo_04)}", Id.Exo_04) { }
		public override string TriNum => "13.3";
		public override string ParashaName => "VaYeled Moshe";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-48.htm";
		public override string Meaning => "Then Moses Departed";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "4:14-6:1", 1616, 1657);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "55:12", 18753, 18753),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "8:26-59", 26408, 26441),];

	}

	private sealed class Exo_06SE : Triennial
	{
		public Exo_06SE() : base($"{nameof(Id.Exo_06)}", Id.Exo_06) { }
		public override string TriNum => "14.1";
		public override string ParashaName => "Va'crya";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-49.htm";
		public override string Meaning => "And I appeared";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "6:2-7:7", 1658, 1693);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "42:8", 18489, 18489),
new VerseRange(BibleBook.Ezekiel, "28:25-29:21", 21183, 21205),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.ThirdJohn, "1:1-7", 30660, 30666),];

	}

	private sealed class Exo_07SE : Triennial
	{
		public Exo_07SE() : base($"{nameof(Id.Exo_07)}", Id.Exo_07) { }
		public override string TriNum => "14.2";
		public override string ParashaName => "Ki Y'daber";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-1/Y1-50.htm";
		public override string Meaning => "When He Speaks";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "7:8-8:15", 1694, 1726);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "22:1-38", 9482, 9519),
new VerseRange(BibleBook.Joel, "3:1-7", 22345, 22351),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "16:1-17", 30956, 30972),];

	}

	private sealed class Exo_08SE : Triennial
	{
		public Exo_08SE() : base($"{nameof(Id.Exo_08)}", Id.Exo_08) { }
		public override string TriNum => "14.3";
		public override string ParashaName => "Hash'kem Baboker";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-01.htm";
		public override string Meaning => "Rise Up Early in the Morning";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "8:16-9:35", 1727, 1778);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "34:1-11", 18305, 18315),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "11:20", 25426, 25426),
new VerseRange(BibleBook.John, "11:47-53", 26571, 26577),
new VerseRange(BibleBook.Revelation, "8:1-9:6", 30829, 30847),];

	}

	private sealed class Exo_10SE : Triennial
	{
		public Exo_10SE() : base($"{nameof(Id.Exo_10)}", Id.Exo_10) { }
		public override string TriNum => "15.1";
		public override string ParashaName => "Bo";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-02.htm";
		public override string Meaning => "Enter/Go";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "10", 1779, 1807);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "6:6", 7338, 7338),
new VerseRange(BibleBook.Isaiah, "19:1", 18006, 18006),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "24:29-44", 23987, 24002),
new VerseRange(BibleBook.Matthew, "25:31", 24040, 24040),
new VerseRange(BibleBook.Matthew, "26:30", 24085, 24085),];

	}

	private sealed class Exo_11SE : Triennial
	{
		public Exo_11SE() : base($"{nameof(Id.Exo_11)}", Id.Exo_11) { }
		public override string TriNum => "15.2";
		public override string ParashaName => "Od Nega Echad";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-03.htm";
		public override string Meaning => "One More Plague";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "11:1-12:28", 1808, 1845);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "46:13-28", 20059, 20074),
new VerseRange(BibleBook.Micah, "7:15", 22680, 22680),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "3:13-25", 26134, 26146),];

	}

	private sealed class Exo_12SE : Triennial
	{
		public Exo_12SE() : base($"{nameof(Id.Exo_12)}", Id.Exo_12) { }
		public override string TriNum => "15.3";
		public override string ParashaName => "Vayhi-Bachatzi";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-04.htm";
		public override string Meaning => "It was at midnight";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "12:29-51", 1846, 1868);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "21:9-12", 18045, 18048),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "6:4-15", 26262, 26273),
new VerseRange(BibleBook.John, "21:1", 26900, 26900),
new VerseRange(BibleBook.Revelation, "18", 30995, 31018),];

	}

	private sealed class Exo_13aSE : Triennial
	{
		public Exo_13aSE() : base($"{nameof(Id.Exo_13a)}", Id.Exo_13a) { }
		public override string TriNum => "15.4";
		public override string ParashaName => "Kadesh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-05.htm";
		public override string Meaning => "Sanctify";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "13:1-20", 1869, 1888);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "46:3-7", 18590, 18594),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "8:6-9:1", 30099, 30107),
new VerseRange(BibleBook.Hebrews, "9:13-15", 30119, 30121),];

	}

	private sealed class Exo_13bSE : Triennial
	{
		public Exo_13bSE() : base($"{nameof(Id.Exo_13b)}", Id.Exo_13b) { }
		public override string TriNum => "16.1";
		public override string ParashaName => "B'Shalach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-06.htm";
		public override string Meaning => "When He let go";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "13:21-15:21", 1889, 1942);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "24:1-4", 6478, 6481),
new VerseRange(BibleBook.Judges, "4:4-5:31", 6604, 6655),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "16:25", 26752, 26752),
new VerseRange(BibleBook.Philippians, "4:4-23", 29447, 29466),];

	}

	private sealed class Exo_15SE : Triennial
	{
		public Exo_15SE() : base($"{nameof(Id.Exo_15)}", Id.Exo_15) { }
		public override string TriNum => "16.2";
		public override string ParashaName => "Yasa Moshe";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-07.htm";
		public override string Meaning => "Moses Caused Them to Journey";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "15:22-16:24", 1943, 1972);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Psalms, "106:7-8", 15659, 15660),
new VerseRange(BibleBook.Isaiah, "49:8-14", 18645, 18651),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Philippians, "4:4-23", 29447, 29466),];

	}

	private sealed class Exo_16SE : Triennial
	{
		public Exo_16SE() : base($"{nameof(Id.Exo_16)}", Id.Exo_16) { }
		public override string TriNum => "16.3";
		public override string ParashaName => "Hayom ki-Shabbat";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-08.htm";
		public override string Meaning => "Today is Sabbath";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "16:25-17:16", 1973, 2000);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "58:13", 18800, 18800),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "6:35-59", 26293, 26317),];

	}

	private sealed class Exo_18SE : Triennial
	{
		public Exo_18SE() : base($"{nameof(Id.Exo_18)}", Id.Exo_18) { }
		public override string TriNum => "17.1";
		public override string ParashaName => "Yithro";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-09.htm";
		public override string Meaning => "Priest/Jethro";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "18:1-19:6", 2001, 2033);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "6", 17771, 17783),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "6:1-35", 26259, 26293),
new VerseRange(BibleBook.John, "6:60-71", 26318, 26329),];

	}

	private sealed class Exo_19SE : Triennial
	{
		public Exo_19SE() : base($"{nameof(Id.Exo_19)}", Id.Exo_19) { }
		public override string TriNum => "17.2";
		public override string ParashaName => "T'daber";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-10.htm";
		public override string Meaning => "You Shall Speak";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "19:7-20:26", 2034, 2078);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "61:6-10", 18850, 18854),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "12:18", 30231, 30231),
new VerseRange(BibleBook.Hebrews, "12:29", 30242, 30242),];

	}

	private sealed class Exo_21SE : Triennial
	{
		public Exo_21SE() : base($"{nameof(Id.Exo_21)}", Id.Exo_21) { }
		public override string TriNum => "18.1";
		public override string ParashaName => "Mishpatim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-11.htm";
		public override string Meaning => "Judgements";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "21:1-22:24", 2079, 2138);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "56:1", 18755, 18755),
new VerseRange(BibleBook.Jeremiah, "34", 19803, 19824),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "7:1-5", 23318, 23322),];

	}

	private sealed class Exo_22SE : Triennial
	{
		public Exo_22SE() : base($"{nameof(Id.Exo_22)}", Id.Exo_22) { }
		public override string TriNum => "18.2";
		public override string ParashaName => "Im-kesef Talveh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "";
		public override string Meaning => "When You Lend";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "22:25-23:33", 2139, 2178);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "48:10", 18625, 18625),
new VerseRange(BibleBook.Isaiah, "49", 18638, 18663),
new VerseRange(BibleBook.Isaiah, "49:3", 18640, 18640),
new VerseRange(BibleBook.Isaiah, "60:17-61:11", 18839, 18855),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "5", 23236, 23283),];

	}

	private sealed class Exo_24SE : Triennial
	{
		public Exo_24SE() : base($"{nameof(Id.Exo_24)}", Id.Exo_24) { }
		public override string TriNum => "18.3";
		public override string ParashaName => "?";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-12.htm";
		public override string Meaning => "?";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "24", 2179, 2196);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "6:11-13", 8908, 8910),
new VerseRange(BibleBook.Isaiah, "60:17-61:9", 18839, 18853),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "26:26-30", 24081, 24085),];

	}

	private sealed class Exo_25SE : Triennial
	{
		public Exo_25SE() : base($"{nameof(Id.Exo_25)}", Id.Exo_25) { }
		public override string TriNum => "19.1";
		public override string ParashaName => "T'rumah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-13.htm";
		public override string Meaning => "Offering";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "25:1-26:30", 2197, 2273);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "66", 18924, 18947),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "7:25-32", 26354, 26361),
new VerseRange(BibleBook.Hebrews, "9", 30107, 30134),];

	}

	private sealed class Exo_27aSE : Triennial
	{
		public Exo_27aSE() : base($"{nameof(Id.Exo_27a)}", Id.Exo_27a) { }
		public override string TriNum => "19.2";
		public override string ParashaName => "V'ashita Parochet";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-13.htm";
		public override string Meaning => "You Shall Make a Partition";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "26:31-27:19", 2274, 2292);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "16:10-19", 20773, 20782),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "23:1-28", 23920, 23947),];

	}

	private sealed class Exo_27bSE : Triennial
	{
		public Exo_27bSE() : base($"{nameof(Id.Exo_27b)}", Id.Exo_27b) { }
		public override string TriNum => "20.1";
		public override string ParashaName => "T'tzaveh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-15.htm";
		public override string Meaning => "You shall command";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "27:20-28:43", 2293, 2337);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "3:10-27", 20513, 20530),
new VerseRange(BibleBook.Ezekiel, "43:10-12", 21583, 21585),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "3", 29997, 30015),];

	}

	private sealed class Exo_29SE : Triennial
	{
		public Exo_29SE() : base($"{nameof(Id.Exo_29)}", Id.Exo_29) { }
		public override string TriNum => "20.2";
		public override string ParashaName => "V'tzeh haDabar";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-16.htm";
		public override string Meaning => "This is the Word";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "29", 2338, 2383);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "61:6", 18850, 18850),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstPeter, "2", 30401, 30425),];

	}

	private sealed class Exo_30aSE : Triennial
	{
		public Exo_30aSE() : base($"{nameof(Id.Exo_30a)}", Id.Exo_30a) { }
		public override string TriNum => "20.3";
		public override string ParashaName => "V'ashit Mizbe'ach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-17.htm";
		public override string Meaning => "You Shall Make an Altar";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "30:1-10", 2384, 2393);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Malachi, "1:11-2:7", 23101, 23111),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "1:1-25", 24895, 24919),];

	}

	private sealed class Exo_30bSE : Triennial
	{
		public Exo_30bSE() : base($"{nameof(Id.Exo_30b)}", Id.Exo_30b) { }
		public override string TriNum => "21.1";
		public override string ParashaName => "Ki Tisa";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-18.htm";
		public override string Meaning => "When you elevate";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "30:11-38", 2394, 2421);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "18:1-39", 9343, 9381),
new VerseRange(BibleBook.SecondKings, "12:5", 9856, 9856),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "5:10-11", 28058, 28059),];

	}

	private sealed class Exo_31SE : Triennial
	{
		public Exo_31SE() : base($"{nameof(Id.Exo_31)}", Id.Exo_31) { }
		public override string TriNum => "21.2";
		public override string ParashaName => "R'eh Qaratiy";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-19.htm";
		public override string Meaning => "See, I Have Called";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "31:1-32:13", 2422, 2452);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "43:7-21", 18513, 18527),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.SecondTimothy, "1", 29811, 29828),];

	}

	private sealed class Exo_32SE : Triennial
	{
		public Exo_32SE() : base($"{nameof(Id.Exo_32)}", Id.Exo_32) { }
		public override string TriNum => "21.3";
		public override string ParashaName => "Vayifen Vayered Moshe";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-20.htm";
		public override string Meaning => "Moses Turned and Went Down";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "32:14-34:26", 2453, 2523);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondSamuel, "22:10-51", 8613, 8654),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "9", 28157, 28189),];

	}

	private sealed class Exo_34SE : Triennial
	{
		public Exo_34SE() : base($"{nameof(Id.Exo_34)}", Id.Exo_34) { }
		public override string TriNum => "22.1";
		public override string ParashaName => "Vayakhel";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-21.htm";
		public override string Meaning => "And He assembled";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "34:27-36:38", 2524, 2605);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "18:27-39", 9369, 9381),
new VerseRange(BibleBook.Jeremiah, "31:31-40", 19723, 19732),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.SecondCorinthians, "3", 28843, 28860),];

	}

	private sealed class Exo_37SE : Triennial
	{
		public Exo_37SE() : base($"{nameof(Id.Exo_37)}", Id.Exo_37) { }
		public override string TriNum => "22.2";
		public override string ParashaName => "Vaya'as B'tzal-el";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-22.htm";
		public override string Meaning => "Betzalel Made";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "37:1-38:20", 2606, 2654);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "7:13-27", 8948, 8962),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "9", 30107, 30134),
new VerseRange(BibleBook.FirstJohn, "1", 30542, 30551),];

	}

	private sealed class Exo_38SE : Triennial
	{
		public Exo_38SE() : base($"{nameof(Id.Exo_38)}", Id.Exo_38) { }
		public override string TriNum => "23.1";
		public override string ParashaName => "Pekude";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-23.htm";
		public override string Meaning => "Accounts of";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "38:21-31", 2655, 2665);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "8:8-22", 8994, 9008),
new VerseRange(BibleBook.Jeremiah, "30:18-24", 19686, 19692),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.SecondCorinthians, "4:1-5:19", 28861, 28897),];

	}

	private sealed class Exo_39SE : Triennial
	{
		public Exo_39SE() : base($"{nameof(Id.Exo_39)}", Id.Exo_39) { }
		public override string TriNum => "23.2";
		public override string ParashaName => "Vumin HaTechelet";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-25.htm";
		public override string Meaning => "From the Blue";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Exodus, "39:1-40:38", 2666, 2746);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "7:13", 8948, 8948),
new VerseRange(BibleBook.Isaiah, "33:20-34:8", 18300, 18312),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "8:1-5", 30094, 30098),
new VerseRange(BibleBook.Revelation, "15", 30948, 30955),];

	}

	private sealed class Lev_01SE : Triennial
	{
		public Lev_01SE() : base($"{nameof(Id.Lev_01)}", Id.Lev_01) { }
		public override string TriNum => "24.1";
		public override string ParashaName => "Vayikra";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-26.htm";
		public override string Meaning => "And He called";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "1 & 2", 2747, 2779);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "43:21-44:23", 18527, 18557),
new VerseRange(BibleBook.Jeremiah, "31:15-20", 19707, 19712),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "3", 28412, 28434),];

	}

	private sealed class Lev_03SE : Triennial
	{
		public Lev_03SE() : base($"{nameof(Id.Lev_03)}", Id.Lev_03) { }
		public override string TriNum => "24.2";
		public override string ParashaName => "Vayim tzevach shalamim karbani";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-27.htm";
		public override string Meaning => "If a Feast Peace-Offering";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "3", 2780, 2796);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "20:33-44", 20929, 20940),
new VerseRange(BibleBook.Ezekiel, "44:10-14", 21610, 21614),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "8", 30829, 30841),];

	}

	private sealed class Lev_04SE : Triennial
	{
		public Lev_04SE() : base($"{nameof(Id.Lev_04)}", Id.Lev_04) { }
		public override string TriNum => "24.3";
		public override string ParashaName => "Nephesh Ki-techeta Mi-shegagah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-28.htm";
		public override string Meaning => "When a Soul Sins Unintentionally";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "4", 2797, 2831);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "18:4-17", 20854, 20867),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "8:1-13", 28118, 28130),
new VerseRange(BibleBook.Hebrews, "10:1-18", 30135, 30152),];

	}

	private sealed class Lev_05SE : Triennial
	{
		public Lev_05SE() : base($"{nameof(Id.Lev_05)}", Id.Lev_05) { }
		public override string TriNum => "24.4";
		public override string ParashaName => "V'nephesh Ki-techeta";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-29.htm";
		public override string Meaning => "Then If a Soul Sins";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "5:1-6:7", 2832, 2857);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "15:1-34", 7562, 7595),
new VerseRange(BibleBook.Zechariah, "5 - 7", 22938, 22977),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "5 & 6", 30781, 30811),];

	}

	private sealed class Lev_06SE : Triennial
	{
		public Lev_06SE() : base($"{nameof(Id.Lev_06)}", Id.Lev_06) { }
		public override string TriNum => "25.1";
		public override string ParashaName => "Tzav";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-30.htm";
		public override string Meaning => "Command";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "6:8-30", 2858, 2880);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "7:12-34", 19132, 19154),
new VerseRange(BibleBook.Ezekiel, "36:16-36", 21376, 21396),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Mark, "12:28-34", 24702, 24708),];

	}

	private sealed class Lev_07SE : Triennial
	{
		public Lev_07SE() : base($"{nameof(Id.Lev_07)}", Id.Lev_07) { }
		public override string TriNum => "25.2";
		public override string ParashaName => "Zeh Karban Aharon";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-31.htm";
		public override string Meaning => "This is the Offering of Aaron";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "7", 2881, 2918);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Malachi, "3:6-9", 23127, 23130),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "23", 23920, 23958),
new VerseRange(BibleBook.John, "6:63-66", 26321, 26324),];

	}

	private sealed class Lev_08SE : Triennial
	{
		public Lev_08SE() : base($"{nameof(Id.Lev_08)}", Id.Lev_08) { }
		public override string TriNum => "25.3";
		public override string ParashaName => "Qach et-Aharon";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-32.htm";
		public override string Meaning => "Take Aaron";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "8", 2919, 2954);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "43:27", 21600, 21600),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "24:29-44", 23987, 24002),
new VerseRange(BibleBook.Matthew, "25:31", 24040, 24040),
new VerseRange(BibleBook.Matthew, "26:30", 24085, 24085),];

	}

	private sealed class Lev_09SE : Triennial
	{
		public Lev_09SE() : base($"{nameof(Id.Lev_09)}", Id.Lev_09) { }
		public override string TriNum => "26.1";
		public override string ParashaName => "Shemeni";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-33.htm";
		public override string Meaning => "Eighth";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "9 - 11", 2955, 3045);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondSamuel, "6:1-7", 8159, 8165),
new VerseRange(BibleBook.FirstKings, "8:54-61", 9040, 9047),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Mark, "7:1-23", 24465, 24487),];

	}

	private sealed class Lev_12SE : Triennial
	{
		public Lev_12SE() : base($"{nameof(Id.Lev_12)}", Id.Lev_12) { }
		public override string TriNum => "27.1";
		public override string ParashaName => "Tazria";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-34.htm";
		public override string Meaning => "She bears seed";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "12:1-13:28", 3046, 3081);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondKings, "4:42-5:19", 9646, 9667),
new VerseRange(BibleBook.Isaiah, "66:7", 18930, 18930),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "1:18-24", 23163, 23169),
new VerseRange(BibleBook.John, "7:37-44", 26366, 26373),];

	}

	private sealed class Lev_13SE : Triennial
	{
		public Lev_13SE() : base($"{nameof(Id.Lev_13)}", Id.Lev_13) { }
		public override string TriNum => "27.2";
		public override string ParashaName => "B'rosh u B'tzaqan";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-35.htm";
		public override string Meaning => "In the Scalp or In the Beard";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "13:29-59", 3082, 3112);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "2:11-18", 29979, 29996),];

	}

	private sealed class Lev_14SE : Triennial
	{
		public Lev_14SE() : base($"{nameof(Id.Lev_14)}", Id.Lev_14) { }
		public override string TriNum => "28.1";
		public override string ParashaName => "Metzorah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-36.htm";
		public override string Meaning => "Infected One";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "14", 3113, 3169);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondKings, "7:1-16", 9709, 9724),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "8:1-4", 23347, 23350),
new VerseRange(BibleBook.Luke, "5:12-14", 25120, 25122),
new VerseRange(BibleBook.Ephesians, "4", 29274, 29305),];

	}

	private sealed class Lev_15SE : Triennial
	{
		public Lev_15SE() : base($"{nameof(Id.Lev_15)}", Id.Lev_15) { }
		public override string TriNum => "28.2";
		public override string ParashaName => "Ish Ish Chai";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-37.htm";
		public override string Meaning => "When Any Man";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "15", 3170, 3202);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "43:18-27", 21591, 21600),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Philippians, "3", 29423, 29443),];

	}

	private sealed class Lev_16SE : Triennial
	{
		public Lev_16SE() : base($"{nameof(Id.Lev_16)}", Id.Lev_16) { }
		public override string TriNum => "30.1";
		public override string ParashaName => "Achare Mot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-38.htm";
		public override string Meaning => "After the death";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "16", 3203, 3236);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "44:1-15", 21601, 21615),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Jude, "1:6-21", 30679, 30694),];

	}

	private sealed class Lev_17SE : Triennial
	{
		public Lev_17SE() : base($"{nameof(Id.Lev_17)}", Id.Lev_17) { }
		public override string TriNum => "30.2";
		public override string ParashaName => "Asher Yishchat";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-39.htm";
		public override string Meaning => "Who Slaughters";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "17", 3237, 3252);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "22:1-19", 20978, 20996),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstJohn, "5", 30626, 30646),];

	}

	private sealed class Lev_18SE : Triennial
	{
		public Lev_18SE() : base($"{nameof(Id.Lev_18)}", Id.Lev_18) { }
		public override string TriNum => "30.3";
		public override string ParashaName => "K'maaseh Eretz";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-40.htm";
		public override string Meaning => "The Practice in the Land";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "18", 3253, 3282);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "22:20-31", 20997, 21008),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "5:1-6:10", 28456, 28488),];

	}

	private sealed class Lev_19SE : Triennial
	{
		public Lev_19SE() : base($"{nameof(Id.Lev_19)}", Id.Lev_19) { }
		public override string TriNum => "31.1";
		public override string ParashaName => "Kedoshim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-41.htm";
		public override string Meaning => "Set apart Ones";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "19 & 20", 3283, 3346);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "4", 17735, 17740),
new VerseRange(BibleBook.Amos, "9", 22497, 22511),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "18", 23729, 23763),];

	}

	private sealed class Lev_21SE : Triennial
	{
		public Lev_21SE() : base($"{nameof(Id.Lev_21)}", Id.Lev_21) { }
		public override string TriNum => "32.1";
		public override string ParashaName => "Emor";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-42.htm";
		public override string Meaning => "Say";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "21", 3347, 3370);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "44", 21601, 21631),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "7:17", 28505, 28505),];

	}

	private sealed class Lev_22SE : Triennial
	{
		public Lev_22SE() : base($"{nameof(Id.Lev_22)}", Id.Lev_22) { }
		public override string TriNum => "32.2";
		public override string ParashaName => "Daber elAharon";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-43.htm";
		public override string Meaning => "Tell Aaron";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "22 & 23", 3371, 3447);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "56", 18755, 18766),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "15:20-23", 28739, 28742),];

	}

	private sealed class Lev_24SE : Triennial
	{
		public Lev_24SE() : base($"{nameof(Id.Lev_24)}", Id.Lev_24) { }
		public override string TriNum => "32.3";
		public override string ParashaName => "Shemen Zayot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-44.htm";
		public override string Meaning => "Oil of Pressed Olives";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "24", 3448, 3470);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Hosea, "14", 22284, 22292),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "7", 26330, 26382),];

	}

	private sealed class Lev_25aSE : Triennial
	{
		public Lev_25aSE() : base($"{nameof(Id.Lev_25a)}", Id.Lev_25a) { }
		public override string TriNum => "33.1";
		public override string ParashaName => "Behar";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-45.htm";
		public override string Meaning => "On the mount";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "25:1-38", 3471, 3508);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "32:6-27", 19738, 19759),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "10:1-33", 26483, 26515),];

	}

	private sealed class Lev_25bSE : Triennial
	{
		public Lev_25bSE() : base($"{nameof(Id.Lev_25b)}", Id.Lev_25b) { }
		public override string TriNum => "33.2";
		public override string ParashaName => "V'ki Amuk";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-46.htm";
		public override string Meaning => "If Impoverished";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "25:39-26:2", 3509, 3527);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "12:17-28", 20698, 20709),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Galatians, "4:1-5:1", 29133, 29164),];

	}

	private sealed class Lev_26SE : Triennial
	{
		public Lev_26SE() : base($"{nameof(Id.Lev_26)}", Id.Lev_26) { }
		public override string TriNum => "34.1";
		public override string ParashaName => "B'chukkotal";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-47.htm";
		public override string Meaning => "In My statutes";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Leviticus, "26:3-27:34", 3528, 3605);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "16:16-17:4", 19353, 19362),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "15", 26701, 26727),];

	}

	private sealed class Num_01SE : Triennial
	{
		public Num_01SE() : base($"{nameof(Id.Num_01)}", Id.Num_01) { }
		public override string TriNum => "35.1";
		public override string ParashaName => "Bemidar";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-48.htm";
		public override string Meaning => "In the wilderness";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "1:1-2:13", 3606, 3672);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "47:13-23", 21693, 21703),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "7", 30812, 30828),];

	}

	private sealed class Num_02SE : Triennial
	{
		public Num_02SE() : base($"{nameof(Id.Num_02)}", Id.Num_02) { }
		public override string TriNum => "35.2";
		public override string ParashaName => "Ish Al Diglo";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-49.htm";
		public override string Meaning => "Each Man by His Own Banner";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "2:14-3:13", 3673, 3706);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "8:18-22", 17826, 17830),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "15:1-7", 25590, 25596),];

	}

	private sealed class Num_03SE : Triennial
	{
		public Num_03SE() : base($"{nameof(Id.Num_03)}", Id.Num_03) { }
		public override string TriNum => "35.3";
		public override string ParashaName => "V'eleh Toldot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-51.htm";
		public override string Meaning => "These are the Generations";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "3:14-4:20", 3707, 3764);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "43:8-13", 18514, 18519),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "5:1-16", 27061, 27076),
new VerseRange(BibleBook.Hebrews, "12", 30214, 30242),];

	}

	private sealed class Num_04SE : Triennial
	{
		public Num_04SE() : base($"{nameof(Id.Num_04)}", Id.Num_04) { }
		public override string TriNum => "37.1";
		public override string ParashaName => "Naso";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-2/Y2-53.htm";
		public override string Meaning => "Elevate";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "4:21-5:10", 3765, 3803);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Judges, "13:2-25", 6887, 6910),
new VerseRange(BibleBook.FirstSamuel, "6:10-16", 7342, 7348),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "12:12-18", 28647, 28653),];

	}

	private sealed class Num_05SE : Triennial
	{
		public Num_05SE() : base($"{nameof(Id.Num_05)}", Id.Num_05) { }
		public override string TriNum => "37.2";
		public override string ParashaName => "Ki Tisteh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-01.htm";
		public override string Meaning => "If She Goes Astray";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "5:11-31", 3804, 3824);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Hosea, "4:14-15", 22148, 22149),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.SecondPeter, "2", 30502, 30523),];

	}

	private sealed class Num_06aSE : Triennial
	{
		public Num_06aSE() : base($"{nameof(Id.Num_06a)}", Id.Num_06a) { }
		public override string TriNum => "37.3";
		public override string ParashaName => "Ki Yafli Lindor";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-02.htm";
		public override string Meaning => "If . . . Make a Special Vow";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "6:1-21", 3825, 3845);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "1:7-11", 7220, 7224),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "1:8-17", 24902, 24911),];

	}

	private sealed class Num_06bSE : Triennial
	{
		public Num_06bSE() : base($"{nameof(Id.Num_06b)}", Id.Num_06b) { }
		public override string TriNum => "37.4";
		public override string ParashaName => "Ko haBarchu";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-03.htm";
		public override string Meaning => "Thus Shall You Bless";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "6:22-7:89", 3846, 3940);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "31:21-34", 19713, 19726),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "13:1-30", 26632, 26661),];

	}

	private sealed class Num_08SE : Triennial
	{
		public Num_08SE() : base($"{nameof(Id.Num_08)}", Id.Num_08) { }
		public override string TriNum => "38.1";
		public override string ParashaName => "B'ha'alot'cha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-04.htm";
		public override string Meaning => "In your going up";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "8-9", 3941, 3989);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Zechariah, "4", 22924, 22937),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Revelation, "11", 30874, 30892),];

	}

	private sealed class Num_10SE : Triennial
	{
		public Num_10SE() : base($"{nameof(Id.Num_10)}", Id.Num_10) { }
		public override string TriNum => "38.2";
		public override string ParashaName => "Avo-yamaim vo-chodesh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-05.htm";
		public override string Meaning => "Whether days or a month";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "10", 3990, 4025);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "27:7-12", 7938, 7943),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstCorinthians, "14:1-9", 28680, 28688),];

	}

	private sealed class Num_11SE : Triennial
	{
		public Num_11SE() : base($"{nameof(Id.Num_11)}", Id.Num_11) { }
		public override string TriNum => "38.3";
		public override string ParashaName => "Ha'am Bemitun'nim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-06.htm";
		public override string Meaning => "The People Sought Complaints";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "11", 4026, 4060);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "12:3-12", 27341, 27350),
new VerseRange(BibleBook.FirstCorinthians, "10", 28569, 28601),];

	}

	private sealed class Num_12SE : Triennial
	{
		public Num_12SE() : base($"{nameof(Id.Num_12)}", Id.Num_12) { }
		public override string TriNum => "38.4";
		public override string ParashaName => "Vatdaber Miryam vAharon";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-07.htm";
		public override string Meaning => "Miriam and Aaron spoke";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "12", 4061, 4076);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "11:22-13:4", 28232, 28271),
new VerseRange(BibleBook.Isaiah, "59:1-21", 18802, 18822),];

	}

	private sealed class Num_13SE : Triennial
	{
		public Num_13SE() : base($"{nameof(Id.Num_13)}", Id.Num_13) { }
		public override string TriNum => "39.1";
		public override string ParashaName => "Shelah L'cha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-08.htm";
		public override string Meaning => "Send for yourself";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "13", 4077, 4109);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "2:1-11", 5871, 5881),
new VerseRange(BibleBook.Joshua, "18:1-2", 6295, 6296),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "11:24-31", 30197, 30204),];

	}

	private sealed class Num_14SE : Triennial
	{
		public Num_14SE() : base($"{nameof(Id.Num_14)}", Id.Num_14) { }
		public override string TriNum => "39.2";
		public override string ParashaName => "Vayibchu haAm";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-09.htm";
		public override string Meaning => "The People Wept";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "14", 4110, 4154);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.SecondChronicles, "36:5-23", 11999, 12017),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Philippians, "3", 29423, 29443),];

	}

	private sealed class Num_15SE : Triennial
	{
		public Num_15SE() : base($"{nameof(Id.Num_15)}", Id.Num_15) { }
		public override string TriNum => "39.3";
		public override string ParashaName => "Ki Tavo'u el Eretz";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-10.htm";
		public override string Meaning => "When You Come into the Land";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "15", 4155, 4195);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "17:19-27", 19377, 19385),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstJohn, "2 and 3", 30552, 30604),];

	}

	private sealed class Num_16SE : Triennial
	{
		public Num_16SE() : base($"{nameof(Id.Num_16)}", Id.Num_16) { }
		public override string TriNum => "40.1";
		public override string ParashaName => "Korach";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-11.htm";
		public override string Meaning => "Korah";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "16", 4196, 4245);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "11:14-12:22", 7460, 7483),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.SecondTimothy, "2:8-21", 29836, 29849),
new VerseRange(BibleBook.Jude, "1", 30674, 30698),];

	}

	private sealed class Num_17SE : Triennial
	{
		public Num_17SE() : base($"{nameof(Id.Num_17)}", Id.Num_17) { }
		public override string TriNum => "40.2";
		public override string ParashaName => "V'qach Meitam Mattah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-12.htm";
		public override string Meaning => "Take from Them a Staff";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "17 & 18", 4246, 4290);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "44:15-28", 21615, 21628),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "5:1-6", 30032, 30037),];

	}

	private sealed class Num_19SE : Triennial
	{
		public Num_19SE() : base($"{nameof(Id.Num_19)}", Id.Num_19) { }
		public override string TriNum => "41.1";
		public override string ParashaName => "Chukkat";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-13.htm";
		public override string Meaning => "Ordinance of";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "19:1-20:13", 4291, 4325);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Judges, "11:1-33", 6831, 6863),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "20:1-31", 26869, 26899),];

	}

	private sealed class Num_20SE : Triennial
	{
		public Num_20SE() : base($"{nameof(Id.Num_20)}", Id.Num_20) { }
		public override string TriNum => "41.2";
		public override string ParashaName => "Malachim MiKadesh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-14.htm";
		public override string Meaning => "Messengers from Kadesh";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "20:14-22:1", 4326, 4377);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "44:29-31", 21629, 21631),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "20:1-31", 26122, 26152),];

	}

	private sealed class Num_22SE : Triennial
	{
		public Num_22SE() : base($"{nameof(Id.Num_22)}", Id.Num_22) { }
		public override string TriNum => "42.1";
		public override string ParashaName => "Balak";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-15.htm";
		public override string Meaning => "Destoyer";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "22:2-23:1", 4378, 4418);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Micah, "5:6-6:8", 22640, 22657),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstPeter, "5:5-7", 30471, 30473),];

	}

	private sealed class Num_23SE : Triennial
	{
		public Num_23SE() : base($"{nameof(Id.Num_23)}", Id.Num_23) { }
		public override string TriNum => "42.2";
		public override string ParashaName => "V'yaas Balaq";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-16.htm";
		public override string Meaning => "Balak Did";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "23:2-25:9", 4419, 4481);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "17:1-18", 6277, 6294),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.James, "3:1-4:17", 30321, 30355),];

	}

	private sealed class Num_25SE : Triennial
	{
		public Num_25SE() : base($"{nameof(Id.Num_25)}", Id.Num_25) { }
		public override string TriNum => "43.1";
		public override string ParashaName => "Pin'has";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-17.htm";
		public override string Meaning => "Dark Skin";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "25:10-26:51", 4482, 4541);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstKings, "18:46-19:21", 9388, 9409),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "23:1-12", 23920, 23931),
new VerseRange(BibleBook.Ephesians, "4:1-16", 29274, 29289),];

	}

	private sealed class Num_26SE : Triennial
	{
		public Num_26SE() : base($"{nameof(Id.Num_26)}", Id.Num_26) { }
		public override string TriNum => "43.2";
		public override string ParashaName => "L'eleh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-18.htm";
		public override string Meaning => "To These";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "26:52-27:23", 4542, 4578);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Malachi, "2:1-9", 23105, 23113),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Ephesians, "1:3-23", 29210, 29230),];

	}

	private sealed class Num_28SE : Triennial
	{
		public Num_28SE() : base($"{nameof(Id.Num_28)}", Id.Num_28) { }
		public override string TriNum => "43.3";
		public override string ParashaName => "Qarbani";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-19.htm";
		public override string Meaning => "My Offerings";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "28 & 29", 4579, 4649);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "45:13-25", 21644, 21656),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Hebrews, "10:1-39", 30135, 30173),];

	}

	private sealed class Num_30SE : Triennial
	{
		public Num_30SE() : base($"{nameof(Id.Num_30)}", Id.Num_30) { }
		public override string TriNum => "44.1";
		public override string ParashaName => "Matot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-20.htm";
		public override string Meaning => "Tribes";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "30 & 31", 4650, 4719);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "1:1-2:3", 18948, 18969),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "5:33-37", 23268, 23272),];

	}

	private sealed class Num_32SE : Triennial
	{
		public Num_32SE() : base($"{nameof(Id.Num_32)}", Id.Num_32) { }
		public override string TriNum => "44.2";
		public override string ParashaName => "Umiqneh Rav";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-21.htm";
		public override string Meaning => "Much Cattle";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "32", 4720, 4761);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "2", 18967, 19003),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.James, "2:1-26", 30295, 30320),];

	}

	private sealed class Num_33SE : Triennial
	{
		public Num_33SE() : base($"{nameof(Id.Num_33)}", Id.Num_33) { }
		public override string TriNum => "45.1";
		public override string ParashaName => "Masa'ei";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-22.htm";
		public override string Meaning => "Stages";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "33", 4762, 4817);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "3", 19004, 19028),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Acts, "6", 27103, 27117),
new VerseRange(BibleBook.James, "4:1-12", 30339, 30350),];

	}

	private sealed class Num_34SE : Triennial
	{
		public Num_34SE() : base($"{nameof(Id.Num_34)}", Id.Num_34) { }
		public override string TriNum => "45.2";
		public override string ParashaName => "Zot haAretz";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-23.htm";
		public override string Meaning => "This is the Land";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "34:1-35:8", 4818, 4854);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "4", 19029, 19059),
new VerseRange(BibleBook.Ezekiel, "45:1-8", 21632, 21639),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Ephesians, "2:1-22", 29231, 29252),];

	}

	private sealed class Num_35SE : Triennial
	{
		public Num_35SE() : base($"{nameof(Id.Num_35)}", Id.Num_35) { }
		public override string TriNum => "45.3";
		public override string ParashaName => "Ki Atem Ovrim et haYarden";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-24.htm";
		public override string Meaning => "When You Cross the Jordan";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Numbers, "35:9-36:13", 4855, 4893);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "5", 19060, 19090),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "4:1-45", 26158, 26202),];

	}

	private sealed class Deu_01SE : Triennial
	{
		public Deu_01SE() : base($"{nameof(Id.Deu_01)}", Id.Deu_01) { }
		public override string TriNum => "46.1";
		public override string ParashaName => "Devarim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-25.htm";
		public override string Meaning => "Words";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "1", 4894, 4939);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "1:1-27", 17656, 17682),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.FirstTimothy, "3:1-7", 29733, 29739),
new VerseRange(BibleBook.Hebrews, "3", 29997, 30015),];

	}

	private sealed class Deu_02SE : Triennial
	{
		public Deu_02SE() : base($"{nameof(Id.Deu_02)}", Id.Deu_02) { }
		public override string TriNum => "46.2";
		public override string ParashaName => "Vanephen";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-26.htm";
		public override string Meaning => "We Turned";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "2:1-3:22", 4940, 4998);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "9", 28157, 28189),];

	}

	private sealed class Deu_03SE : Triennial
	{
		public Deu_03SE() : base($"{nameof(Id.Deu_03)}", Id.Deu_03) { }
		public override string TriNum => "47.1";
		public override string ParashaName => "Va’etchanan";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-27.htm";
		public override string Meaning => "And I pleaded";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "3:23-29", 4999, 5005);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "40:1-11", 18422, 18432),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Mark, "12:28-34", 24702, 24708),
new VerseRange(BibleBook.Romans, "2:1-3:31", 27964, 28023),];

	}

	private sealed class Deu_04SE : Triennial
	{
		public Deu_04SE() : base($"{nameof(Id.Deu_04)}", Id.Deu_04) { }
		public override string TriNum => "47.2";
		public override string ParashaName => "Yisrael, Sh'ma";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-28.htm";
		public override string Meaning => "Israel, Listen";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "4", 5006, 5054);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "40:12-14", 18433, 18435),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "3:1", 25027, 25027),];

	}

	private sealed class Deu_05SE : Triennial
	{
		public Deu_05SE() : base($"{nameof(Id.Deu_05)}", Id.Deu_05) { }
		public override string TriNum => "47.3";
		public override string ParashaName => "Vayiqra Moshe";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-29.htm";
		public override string Meaning => "Moses Summoned";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "5:1-6:3", 5055, 5090);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "40:15-26", 18436, 18447),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "10:1", 28190, 28190),];

	}

	private sealed class Deu_06SE : Triennial
	{
		public Deu_06SE() : base($"{nameof(Id.Deu_06)}", Id.Deu_06) { }
		public override string TriNum => "47.4";
		public override string ParashaName => "Vazot haMitzvot";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-30.htm";
		public override string Meaning => "This is the Commandment";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "6:4-7:26", 5091, 5138);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "4:1", 23211, 23211),];

	}

	private sealed class Deu_08SE : Triennial
	{
		public Deu_08SE() : base($"{nameof(Id.Deu_08)}", Id.Deu_08) { }
		public override string TriNum => "48.1";
		public override string ParashaName => "Ekev";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-31.htm";
		public override string Meaning => "On the heel of (because)";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "8", 5139, 5158);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "49:14-51:3", 18651, 18677),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "4:11", 25075, 25075),
new VerseRange(BibleBook.Romans, "8:31-39", 28148, 28156),];

	}

	private sealed class Deu_09SE : Triennial
	{
		public Deu_09SE() : base($"{nameof(Id.Deu_09)}", Id.Deu_09) { }
		public override string TriNum => "48.2";
		public override string ParashaName => "Atah Over Hayom";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-32.htm";
		public override string Meaning => "You are Crossing Over Today";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "9", 5159, 5187);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Ephesians, "2:11", 29241, 29241),];

	}

	private sealed class Deu_10SE : Triennial
	{
		public Deu_10SE() : base($"{nameof(Id.Deu_10)}", Id.Deu_10) { }
		public override string TriNum => "48.3";
		public override string ParashaName => "Pesal Lecha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-33.htm";
		public override string Meaning => "Carve for Yourself";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "10:1-11:25", 5188, 5234);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Colossians, "3:1", 29519, 29519),];

	}

	private sealed class Deu_11SE : Triennial
	{
		public Deu_11SE() : base($"{nameof(Id.Deu_11)}", Id.Deu_11) { }
		public override string TriNum => "49.1";
		public override string ParashaName => "Re'eh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-34.htm";
		public override string Meaning => "See!";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "11:26-12:19", 5235, 5260);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "54:11-55:5", 18735, 18746),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "6:20", 25167, 25167),
new VerseRange(BibleBook.FirstJohn, "4:1-6", 30605, 30610),];

	}

	private sealed class Deu_12SE : Triennial
	{
		public Deu_12SE() : base($"{nameof(Id.Deu_12)}", Id.Deu_12) { }
		public override string TriNum => "49.2";
		public override string ParashaName => "Ki-Yarchiv";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-35.htm";
		public override string Meaning => "When He Enlarges";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "12:20-15:6", 5261, 5326);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "7:15", 23332, 23332),];

	}

	private sealed class Deu_15SE : Triennial
	{
		public Deu_15SE() : base($"{nameof(Id.Deu_15)}", Id.Deu_15) { }
		public override string TriNum => "49.3";
		public override string ParashaName => "Ev-yon";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-36.htm";
		public override string Meaning => "A Poor Man";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "15:7-16:17", 5327, 5360);

		public override List<VerseRange>? HaftorahVerses => null;
		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "4:14", 25078, 25078),];

	}

	private sealed class Deu_16SE : Triennial
	{
		public Deu_16SE() : base($"{nameof(Id.Deu_16)}", Id.Deu_16) { }
		public override string TriNum => "50.1";
		public override string ParashaName => "Shoftim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-37.htm";
		public override string Meaning => "Judges";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "16:18-17:13", 5361, 5378);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "51:12-52:12", 18686, 18709),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "5:1", 26212, 26212),
new VerseRange(BibleBook.FirstCorinthians, "5:9-13", 28464, 28468),];

	}

	private sealed class Deu_17SE : Triennial
	{
		public Deu_17SE() : base($"{nameof(Id.Deu_17)}", Id.Deu_17) { }
		public override string TriNum => "50.2";
		public override string ParashaName => "Ki haBo";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-38.htm";
		public override string Meaning => "When You Enter";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "17:14-18:13", 5379, 5398);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.FirstSamuel, "8:1-7", 7371, 7377),
new VerseRange(BibleBook.Jeremiah, "31:31-34", 19723, 19726),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "19:11-22", 19723, 19726),
new VerseRange(BibleBook.Acts, "13:13-44", 27376, 27407),];

	}

	private sealed class Deu_18SE : Triennial
	{
		public Deu_18SE() : base($"{nameof(Id.Deu_18)}", Id.Deu_18) { }
		public override string TriNum => "50.3";
		public override string ParashaName => "Navi Miqirb'cha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-39.htm";
		public override string Meaning => "A Prophet from Your Midst";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "18:14-20:9", 5399, 5437);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "29:8-30:9", 19644, 19677),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Galatians, "5:1-6:10", 29164, 29199),];

	}

	private sealed class Deu_20SE : Triennial
	{
		public Deu_20SE() : base($"{nameof(Id.Deu_20)}", Id.Deu_20) { }
		public override string TriNum => "50.4";
		public override string ParashaName => "Ki Tiqrav el Iyr";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-40.htm";
		public override string Meaning => "When You Approach a City";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "20:10-21:9", 5438, 5457);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "24:1-15", 6478, 6492),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "10:11-31", 26493, 26513),];

	}

	private sealed class Deu_21SE : Triennial
	{
		public Deu_21SE() : base($"{nameof(Id.Deu_21)}", Id.Deu_21) { }
		public override string TriNum => "51.1";
		public override string ParashaName => "Ki Tetse";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-41.htm";
		public override string Meaning => "When you go out";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "21:10-22:5", 5458, 5476);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "54:1-10", 18725, 18734),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "20:27-38", 25807, 25818),
new VerseRange(BibleBook.Galatians, "3:1-5:26", 29104, 29189),];

	}

	private sealed class Deu_22SE : Triennial
	{
		public Deu_22SE() : base($"{nameof(Id.Deu_22)}", Id.Deu_22) { }
		public override string TriNum => "51.2";
		public override string ParashaName => "Kein Tzipor";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-42.htm";
		public override string Meaning => "A Bird|s Nest";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "22:6-23:8", 5477, 5509);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Micah, "5:1-6", 22635, 22640),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "22:1-14", 23874, 23887),];

	}

	private sealed class Deu_23aSE : Triennial
	{
		public Deu_23aSE() : base($"{nameof(Id.Deu_23a)}", Id.Deu_23a) { }
		public override string TriNum => "51.3";
		public override string ParashaName => "Ki-tetzei Machaneh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-43.htm";
		public override string Meaning => "When a Camp";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "23:9-20", 5510, 5521);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "1:16-26", 17671, 17681),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "8:1-14", 23347, 23360),];

	}

	private sealed class Deu_23bSE : Triennial
	{
		public Deu_23bSE() : base($"{nameof(Id.Deu_23b)}", Id.Deu_23b) { }
		public override string TriNum => "51.4";
		public override string ParashaName => "Ki-chidor neder";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-44.htm";
		public override string Meaning => "When You Make a Vow";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "23:21-24:18", 5522, 5544);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "19:1-25", 18006, 18030),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "5:30-37", 23265, 23272),];

	}

	private sealed class Deu_24SE : Triennial
	{
		public Deu_24SE() : base($"{nameof(Id.Deu_24)}", Id.Deu_24) { }
		public override string TriNum => "51.5";
		public override string ParashaName => "Ki-tik'tzor";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-45.htm";
		public override string Meaning => "When You Reap";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "24:19-25:19", 5545, 5567);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Hosea, "10:12-15", 22238, 22241),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Luke, "12:13-48", 25473, 25508),];

	}

	private sealed class Deu_26SE : Triennial
	{
		public Deu_26SE() : base($"{nameof(Id.Deu_26)}", Id.Deu_26) { }
		public override string TriNum => "52.1";
		public override string ParashaName => "Ki Tavo";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-46.htm";
		public override string Meaning => "When you enter in";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "26 & 27", 5568, 5612);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "60", 18823, 18844),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "11:1-15", 28211, 28225),
new VerseRange(BibleBook.Revelation, "21:9-22:7", 31063, 31088),];

	}

	private sealed class Deu_28SE : Triennial
	{
		public Deu_28SE() : base($"{nameof(Id.Deu_28)}", Id.Deu_28) { }
		public override string TriNum => "52.2";
		public override string ParashaName => "Im-Shamoa";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-47.htm";
		public override string Meaning => "If You Hearken";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "28:1-29:9", 5613, 5689);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "55:1-5", 18742, 18746),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "14:1-31", 26670, 26700),];

	}

	private sealed class Deu_29SE : Triennial
	{
		public Deu_29SE() : base($"{nameof(Id.Deu_29)}", Id.Deu_29) { }
		public override string TriNum => "53.1";
		public override string ParashaName => "Nitsavim";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-48.htm";
		public override string Meaning => "You are standing";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "29:10-30:10", 5690, 5719);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Isaiah, "55:6-58:8", 18747, 18795),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Romans, "10:1-13", 28190, 28202),];

	}

	private sealed class Deu_30SE : Triennial
	{
		public Deu_30SE() : base($"{nameof(Id.Deu_30)}", Id.Deu_30) { }
		public override string TriNum => "53.2";
		public override string ParashaName => "haMitzvah";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-49.htm";
		public override string Meaning => "This Commandment";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "30:11-31:13", 5720, 5742);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Jeremiah, "12:15-17", 19265, 19267),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "11:7-30", 23467, 23490),];

	}

	private sealed class Deu_31SE : Triennial
	{
		public Deu_31SE() : base($"{nameof(Id.Deu_31)}", Id.Deu_31) { }
		public override string TriNum => "64.1";
		public override string ParashaName => "Vayelekh";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-50.htm";
		public override string Meaning => "And he went";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "31:14-30", 5743, 5759);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Hosea, "14:2-9", 22285, 22292),
new VerseRange(BibleBook.Joel, "2:15-27", 22327, 22339),
new VerseRange(BibleBook.Micah, "7:18-20", 22683, 22685),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "12:35-14:26", 26616, 26695),
new VerseRange(BibleBook.Hebrews, "13:5-8", 30247, 30250),];

	}

	private sealed class Deu_32SE : Triennial
	{
		public Deu_32SE() : base($"{nameof(Id.Deu_32)}", Id.Deu_32) { }
		public override string TriNum => "66.1";
		public override string ParashaName => "Haazinu";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "http://www.ahavta.org/Commentary Y-3/Y3-51.htm";
		public override string Meaning => "Give ear";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "32", 5760, 5811);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "17:22-24", 20848, 20850),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.John, "17:1-26", 26761, 26786),];

	}

	private sealed class Deu_33SE : Triennial
	{
		public Deu_33SE() : base($"{nameof(Id.Deu_33)}", Id.Deu_33) { }
		public override string TriNum => "67.1";
		public override string ParashaName => "VeZot HaBracha";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "";
		public override string Meaning => "This is the blessing";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "33", 5812, 5840);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Ezekiel, "37:15-20", 21413, 21418),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "19:25-20:16", 23788, 23809),
new VerseRange(BibleBook.Jude, "1:8-9", 30681, 30682),];

	}

	private sealed class Deu_34SE : Triennial
	{
		public Deu_34SE() : base($"{nameof(Id.Deu_34)}", Id.Deu_34) { }
		public override string TriNum => "68.1";
		public override string ParashaName => "Vayaal Moshe";
		public override string NameUrl => NameUrl;
		public override string AhavtaURL => "";
		public override string Meaning => "Moses Ascended";
		public override VerseRange TorahVerse => new VerseRange(BibleBook.Deuteronomy, "34", 5841, 5852);

		public override List<VerseRange> HaftorahVerses => [
		new VerseRange(BibleBook.Joshua, "1", 5853, 5870),];

		public override List<VerseRange> BritVerses => [
		new VerseRange(BibleBook.Matthew, "4:5-11", 23215, 23221),];

	}

	#endregion

}
