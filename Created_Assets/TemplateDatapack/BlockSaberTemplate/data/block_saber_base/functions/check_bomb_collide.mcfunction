execute if score @s NoteHealth < #CONST3 Const_3 run say -1 Hit Bomb
execute if score @s NoteHealth < #CONST3 Const_3 at @s run kill @e[type=armor_stand,distance=..0.05]
execute if score @s NoteHealth < #CONST3 Const_3 run kill @s
execute if entity @s[name="Blue Saber"] run say -1 Hit Bomb
execute if entity @s[name="Blue Saber"] at @s run kill @e[type=armor_stand,distance=..0.05]
execute if entity @s[name="Blue Saber"] run kill @s

execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.21] run say -1 Hit Bomb
execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.21] at @s run kill @e[type=armor_stand,tag=node,distance=..0.05]
execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.21] run kill @s