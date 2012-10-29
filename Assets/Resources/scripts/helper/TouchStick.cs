using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
 
 
[RequireComponent(typeof(GUITexture))]
public class TouchStick : MonoBehaviour
{
 
    public bool touchPad = false;
    public bool fadeGUI = false;
    public Vector2 deadZone = Vector2.zero;
    public bool normalize = false;
    public int tapCount = -1;
    private Rect touchZone;
    private int lastFingerId = -1;
    private float tapTimeWindow;
    private Vector2 fingerDownPos;
    private float firstDeltaTime;
 
    private GUITexture gui;
    private Rect defaultRect;
    private Boundary guiBoundary = new Boundary();
    private Vector2 guiTouchOffset;
    private Vector2 guiCenter;
 
    public bool isFingerDown
    {
        get
        {
            return (lastFingerId != -1);
        }
    }
 
    public int latchedFinger
    {
        set
        {
            if (lastFingerId == value)
            {
                Restart();
            }
        }
    }
 
    public static Vector2 position;

 
    private static string joysticksTag = "joystick";
    private static List<Joystick> joysticks;
    private static bool enumeratedJoysticks = false;
    private static float tapTimeDelta = 0.3f;
 
 
    private void Reset()
    {
        try
        {
            gameObject.tag = joysticksTag;
        }
        catch (Exception)
        {
            Debug.LogError("The \""+joysticksTag+"\" tag has not yet been defined in the Tag Manager.");
            throw;
        }
    }
 
 
    private void Awake()
    {
        gui = GetComponent<GUITexture>();
        if (gui.texture == null)
        {
            Debug.LogError("Joystick object requires a valid texture!");
            gameObject.active = false;
            return;
        }
 
        if (!enumeratedJoysticks)
        {
            try
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag(joysticksTag);
                joysticks = new List<Joystick>(objs.Length);
                foreach (GameObject obj in objs)
                {
                    Joystick newJoystick = obj.GetComponent<Joystick>();
                    if (newJoystick == null)
                    {
                        throw new NullReferenceException("Joystick gameObject found without a suitable Joystick component.");
                    }
                    joysticks.Add(newJoystick);
                }
                enumeratedJoysticks = true;
            }
            catch (Exception exp)
            {
                Debug.LogError("Error collecting Joystick objects: "+exp.Message);
                throw;
            }
        }
 
        defaultRect = gui.pixelInset;
 
        defaultRect.x += transform.position.x * Screen.width;// + gui.pixelInset.x; // -  Screen.width * 0.5f;
        defaultRect.y += transform.position.y * Screen.height;// - Screen.height * 0.5f;
 
        transform.position = new Vector3(0, 0, transform.position.z);
 
        if (touchPad)
        {
            touchZone = defaultRect;
        }
        else
        {
            guiTouchOffset.x = defaultRect.width * 0.5f;
            guiTouchOffset.y = defaultRect.height * 0.5f;
 
            guiCenter.x = defaultRect.x + guiTouchOffset.x;
            guiCenter.y = defaultRect.y + guiTouchOffset.y;
 
            guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
            guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
            guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
            guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
        }
 
    }
 
 
    public void Enable()
    {
        enabled = true;
    }
 
 
    public void Disable()
    {
        enabled = false;
    }
 
 
    public void Restart(){
        lastFingerId = -1;
        position = Vector2.zero;
        fingerDownPos = Vector2.zero;
    }
 
 
    private void Update(){
 
        int count = Input.touchCount;
 
        if (tapTimeWindow > 0){
            tapTimeWindow -= Time.deltaTime;
        }
        else{
            tapCount = 0;
        }
 
        if (count == 0){
            Restart();
        }
        else
        {
            for (int i = 0; i < count; i++){
                Touch touch = Input.GetTouch(i);
                Vector2 guiTouchPos = touch.position - guiTouchOffset;
 
                bool shouldLatchFinger = false;
                if (touchPad && touchZone.Contains(touch.position))
                {
                    shouldLatchFinger = true;
                }
                else if (gui.HitTest(touch.position))
                {
                    shouldLatchFinger = true;
                }
 
                if (shouldLatchFinger && (lastFingerId == -1 || lastFingerId != touch.fingerId))
                {
 
                    if (touchPad)
                    {
                        if (fadeGUI)
                        {
                            gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, 0.15f);
                        }
                        lastFingerId = touch.fingerId;
                        fingerDownPos = touch.position;
                    }
 
                    lastFingerId = touch.fingerId;
 
                    if (tapTimeWindow > 0)
                    {
                        tapCount++;
                    }
                    else
                    {
                        tapCount = 1;
                        tapTimeWindow = tapTimeDelta;
                    }
 
                    foreach (Joystick j in joysticks)
                    {
                        if (j == this)
                        {
                            continue;
                        }
                        j.latchedFinger = touch.fingerId;
                    }
                }
 
                if (lastFingerId == touch.fingerId)
                {
        
                    if (touch.tapCount > tapCount)
                    {
                        tapCount = touch.tapCount;
                    }
 
                    if (touchPad)
                    {
                        position = new Vector2
                            (
                              Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2), -1, 1),
                              Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2), -1, 1)
                            );
                    }
                    else
                    {
                        gui.pixelInset = new Rect
                            (
                              Mathf.Clamp(guiTouchPos.x, guiBoundary.min.x, guiBoundary.max.x),
                              Mathf.Clamp(guiTouchPos.y, guiBoundary.min.y, guiBoundary.max.y),
                              gui.pixelInset.width,
                              gui.pixelInset.height
                            );
                    }
 
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        Restart();
                    }
                }
            }
 
        }
 
 
        float absoluteX = Mathf.Abs(position.x);
        float absoluteY = Mathf.Abs(position.y);
    }
 
 
}