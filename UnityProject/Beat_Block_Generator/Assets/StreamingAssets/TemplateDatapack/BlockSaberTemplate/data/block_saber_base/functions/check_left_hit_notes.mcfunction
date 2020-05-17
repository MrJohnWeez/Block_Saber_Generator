execute if score @s NoteHealth < #CONST3 Const_3 run say +1 Correct Left
execute if score @s NoteHealth < #CONST3 Const_3 at @s run kill @e[type=armor_stand,distance=..0.05]
execute if score @s NoteHealth < #CONST3 Const_3 run kill @s
execute if entity @s[name="Blue Saber"] run say -1 Worng Left
execute if entity @s[name="Blue Saber"] at @s run kill @e[type=armor_stand,distance=..0.05]
execute if entity @s[name="Blue Saber"] run kill @s