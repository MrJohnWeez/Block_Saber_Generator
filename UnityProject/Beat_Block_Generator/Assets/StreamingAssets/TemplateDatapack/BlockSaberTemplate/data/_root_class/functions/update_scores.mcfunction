# Update the combo score
setblock 0 140 0 birch_sign
data modify entity @e[type=armor_stand,tag=comboDisplayNumber,limit=1] CustomName set value '""'
data merge block 0 140 0 {Text1:'{"score":{"name":"#BlockSaberGlobal","objective":"Combo"}}'}
data modify entity @e[type=armor_stand,tag=comboDisplayNumber,limit=1] CustomName set from block 0 140 0 Text1
setblock 0 140 0 air

# Update the mutiplier score
setblock 0 140 0 birch_sign
data modify entity @e[type=armor_stand,tag=multiplierValue,limit=1] CustomName set value '""'
data merge block 0 140 0 {Text1:'{"score":{"name":"#BlockSaberGlobal","objective":"Multiplier"}}'}
data modify entity @e[type=armor_stand,tag=multiplierValue,limit=1] CustomName set from block 0 140 0 Text1
setblock 0 140 0 air


# c# code
#   string part1 = "\"[\\\"\\\",{\\\"text\\\":\\\"";
#   		string part2 = "\\\",\\\"color\\\":\\\"white\\\"},{\\\"text\\\":\\\"";
#   		string part3 = "\\\",\\\"color\\\":\\\"dark_gray\\\"}]\"\n\r";
#   		StringBuilder sb = new StringBuilder();
#   		for(int i = 0; i <= 100; i += 2)
#   		{
#   			sb.AppendFormat("execute if score #BlockSaberGlobal HealthPoints matches {0}..{1} run data modify entity @e[type=armor_stand,tag=healthDisplay,#   limit=1] CustomName set value ", i, i+1);
#   			sb.Append(part1);
#   			for(int x = 0; x < i / 2; x++)
#   			{sb.Append("▏");}
#   			sb.Append(part2);
#   			for(int x = 0; x < (100 - i) / 2; x++)
#   			{sb.Append("▏");}
#   			sb.Append(part3);
#   		}
#   		Console.WriteLine(sb.ToString());

execute if score #BlockSaberGlobal HealthPoints matches 0..1 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 2..3 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 4..5 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 6..7 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 8..9 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 10..11 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 12..13 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 14..15 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 16..17 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 18..19 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 20..21 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 22..23 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 24..25 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 26..27 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 28..29 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 30..31 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 32..33 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 34..35 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 36..37 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 38..39 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 40..41 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 42..43 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 44..45 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 46..47 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 48..49 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 50..51 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 52..53 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 54..55 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 56..57 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 58..59 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 60..61 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 62..63 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 64..65 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 66..67 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 68..69 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 70..71 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 72..73 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 74..75 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 76..77 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 78..79 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 80..81 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 82..83 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 84..85 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 86..87 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 88..89 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 90..91 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 92..93 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 94..95 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 96..97 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 98..99 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"▏\",\"color\":\"dark_gray\"}]"
execute if score #BlockSaberGlobal HealthPoints matches 100..101 run data modify entity @e[type=armor_stand,tag=healthDisplay,limit=1] CustomName set value "[\"\",{\"text\":\"▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏▏\",\"color\":\"white\"},{\"text\":\"\",\"color\":\"dark_gray\"}]"

execute if score #BlockSaberGlobal HealthPoints matches ..-1 if score #BlockSaberGlobal XpLevels matches 1 run function _root_class:map_difficulty_failed
execute if score #BlockSaberGlobal HealthPoints matches 100 if score #BlockSaberGlobal HealthPoints matches 1..7 run scoreboard players operation #BlockSaberGlobal XpLevels *= #CONST Const_2
