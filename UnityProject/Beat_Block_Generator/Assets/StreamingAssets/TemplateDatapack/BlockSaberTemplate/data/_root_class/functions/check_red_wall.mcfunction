execute at @e[type=armor_stand,tag=fakePlayerEyes,tag=blockBeat] if entity @s[distance=..0.2] at @p[scores={PlayerPlaying=1}] run function _root_class:player_in_wall