using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Environment and lighting event </summary>
    [Serializable]
    public class Event
    {
        /// <summary> Time offset in beats </summary>
        [SerializeField] private float _time;
        /// <summary> Type of this event </summary>
        /// <remarks>
        /// EventTypeInvalid (-1)
        /// <list type="number"> Controls lights in the Back Lasers group. (0)
        /// <item> Controls lights in the Ring Lights group. (1) </item>
        /// <item> Controls lights in the Left Rotating Lasers group. (2) </item>
        /// <item> Controls lights in the Right Rotating Lasers group. (3) </item>
        /// <item> Controls lights in the Center Lights group. (4) </item>
        /// <item> (Previously unused) Controls boost light colors (secondary colors). (5) </item>
        /// <item> (Previously unused) Controls extra left side lights in some environments. (6) </item>
        /// <item> (Previously unused) Controls extra right side lights in some environments. (7) </item>
        /// <item> Creates one ring spin in the environment. (8) </item>
        /// <item> Controls zoom for applicable rings. Is not affected by _value. (9) </item>
        /// <item> (Previously unused) (Previously Official BPM Changes.) Billie environment - Controls left side lasers (10) </item>
        /// <item> (Previously unused) Billie environment - Controls right side lasers. (11) </item>
        /// <item> Controls rotation speed for applicable lights in Left Rotating Lasers. (12) </item>
        /// <item> Controls rotation speed for applicable lights in Right Rotating Lasers. (13) </item>
        /// <item> (Previously unused) 360/90 Early rotation. Rotates future objects, while also rotating objects at the same time. (14) </item>
        /// <item> (Previously unused) 360/90 Late rotation. Rotates future objects, but ignores rotating objects at the same time. (15) </item>
        /// <item> Interscope environment - Lowers car hydraulics Gaga environment - Controls middle left tower height (16) </item>
        /// <item> Interscope environment - Raises car hydraulics Gaga environment - Controls middle right tower height (17) </item>
        /// <item> Gaga environment - Controls outer left tower height (18) </item>
        /// <item> Gaga environment - Controls outer right tower height (19) </item>
        /// </list>
        /// </remarks>
        [SerializeField] private int _type;
        /// <summary> Parameter value for this event </summary>
        /// <remarks>
        /// EventTypeInvalid (-1)
        /// <list type="number"> Turns the light group off. (0)
        /// <item> Changes the lights to blue, and turns the lights on. (1) </item>
        /// <item> Changes the lights to blue, and flashes brightly before returning to normal (2) </item>
        /// <item> Changes the lights to blue, and flashes brightly before fading to black. (3) </item>
        /// <item> 	(Previously Unused.) Changes the lights to blue by fading from the current state. (4) </item>
        /// <item> Changes the lights to red, and turns the lights on. (5) </item>
        /// <item> Changes the lights to red, and flashes brightly before returning to normal. (6) </item>
        /// <item> Changes the lights to red, and flashes brightly before fading to black. (7) </item>
        /// <item> Changes the lights to red by fading from the current state. (8) </item>
        /// </list>
        /// _value 4 and 8 were introduced in Beat Saber version 1.18.0 (Billie Eilish patch).
        /// These events will only transition from Off and On (0, 1, and 4 )events.
        /// They will do nothing if transitions fade and flash events (2, 3, 6, and 7)
        /// <para> Controlling Boost Colors:
        /// <list type="number"> Turns the event off - switches to first (default) pair of colors.(0)
        /// <item> Turns the event on - switches to second pair of colors (1) </item>
        /// </list>
        /// </para>
        /// <para> Controlling Rings:
        /// When the event is used to control ring zoom, the _value of the event does nothing.
        /// When the event is used to control ring spin, the _value only affects cars in the Interscope environment and does nothing in other environments
        /// </para>
        /// <para> Controlling Boost Colors:
        /// <list type="number"> Affects all the cars. Does not affect hydraulics.(0)
        /// <item> Affects all the cars. (1) </item>
        /// <item> Affects the left cars. (2) </item>
        /// <item> Affects the right cars. (3) </item>
        /// <item> Affects the front-most cars. (4) </item>
        /// <item> Affects the front-middle cars. (5) </item>
        /// <item> Affects the back-middle cars. (6) </item>
        /// <item> Affects the back-most cars. (7) </item>
        /// </list>
        /// </para>
        /// <para> Controlling Laser Rotation Speed:
        /// When the event is used to control laser speed for a group of lights, the _value is used as a multiplier to their base rotational velocity.
        /// If _value is 0, the random rotation offset for each laser will also be reset, causing all rotating lasers to line up perfectly.
        /// </para>
        /// <para> Controlling 360/90 Rotation: When the event is used to control rotation in a 360/90 degree level, the _value is used to add rotation equal to the following table:
        /// <list type="number"> 60 Degrees Counterclockwise (0)
        /// <item> 45 Degrees Counterclockwise (1) </item>
        /// <item> 30 Degrees Counterclockwise (2) </item>
        /// <item> 15 Degrees Counterclockwise (3) </item>
        /// <item> 15 Degrees Clockwise (4) </item>
        /// <item> 30 Degrees Clockwise (5) </item>
        /// <item> 45 Degrees Clockwise (6) </item>
        /// <item> 60 Degrees Clockwise (7) </item>
        /// </list>
        /// </para>
        /// </remarks>
        [SerializeField] private int _value;
        /// <summary> Environment / lighting event custom data </summary>
        [SerializeField] private CustomEventData _customData;


        /// <inheritdoc cref="_time" />
        public float Time { get => _time; set => _time = value; }

        /// <inheritdoc cref="_type" />
        public int Type { get => _type; set => _type = value; }

        /// <inheritdoc cref="_value" />
        public int Value { get => _value; set => _value = value; }

        /// <inheritdoc cref="_customData" />
        public CustomEventData CustomData { get => _customData; set => _customData = value; }
    }
}
