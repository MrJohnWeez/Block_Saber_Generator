execute if score @s NoteHealth < #CONST3 Const_3 run function _block_saber_base:note_was_wrong
execute if entity @s[name="Blue Saber"] run function _block_saber_base:note_was_wrong
execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.21] run function _block_saber_base:note_was_wrong