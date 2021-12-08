﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;
using System.Runtime.InteropServices;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    public TextMeshProUGUI MemoryText;

    private float pollingTime = 0.5f;
    private float time;
    private int frameCount;


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            FpsText.text = frameRate.ToString() + " FPS";
            MemoryText.text = ("Total memory: " + FormatSize(GetTotalPhys()) + "\n" + "Used: " + FormatSize(GetUsedPhys()) + "\n" + "Available: " + FormatSize(GetAvailPhys()));

            //Console.WriteLine("It can be used:" + FormatSize(GetAvailPhys()));

            time -= pollingTime;
            frameCount = 0;
        }
    }
       

    #region Obtain memory information API
    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GlobalMemoryStatusEx(ref MEMORY_INFO mi);

    //Define the information structure of memory
    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint dwLength; //Current structure size
        public uint dwMemoryLoad; //Current memory utilization
        public ulong ullTotalPhys; //Total physical memory size
        public ulong ullAvailPhys; //Available physical memory size
        public ulong ullTotalPageFile; //Total Exchange File Size
        public ulong ullAvailPageFile; //Total Exchange File Size
        public ulong ullTotalVirtual; //Total virtual memory size
        public ulong ullAvailVirtual; //Available virtual memory size
        public ulong ullAvailExtendedVirtual; //Keep this value always zero
    }
    #endregion

    #region Formatting capacity size
    /// <summary>
    /// Formatting capacity size
    /// </summary>
    /// <param name="size">Capacity ( B)</param>
    /// <returns>Formatted capacity</returns>
    private static string FormatSize(double size)
    {
        double d = (double)size;
        int i = 0;
        while ((d > 1024) && (i < 5))
        {
            d /= 1024;
            i++;
        }
        string[] unit = { "B", "KB", "MB", "GB", "TB" };
        return (string.Format("{0} {1}", Math.Round(d, 2), unit[i]));
    }
    #endregion

    #region Get the current memory usage
    /// <summary>
    /// Get the current memory usage
    /// </summary>
    /// <returns></returns>
    public static MEMORY_INFO GetMemoryStatus()
    {
        MEMORY_INFO mi = new MEMORY_INFO();
        mi.dwLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(mi);
        GlobalMemoryStatusEx(ref mi);
        return mi;
    }
    #endregion

    #region Get the current available physical memory size
    /// <summary>
    /// Get the current available physical memory size
    /// </summary>
    /// <returns>Current available physical memory( B)</returns>
    public static ulong GetAvailPhys()
    {
        MEMORY_INFO mi = GetMemoryStatus();
        return mi.ullAvailPhys;
    }
    #endregion

    #region Get the current memory size used
    /// <summary>
    /// Get the current memory size used
    /// </summary>
    /// <returns>Memory size used( B)</returns>
    public static ulong GetUsedPhys()
    {
        MEMORY_INFO mi = GetMemoryStatus();
        return (mi.ullTotalPhys - mi.ullAvailPhys);
    }
    #endregion

    #region Get the current total physical memory size
    /// <summary>
    /// Get the current total physical memory size
    /// </summary>
    /// <returns&amp;gt;Total physical memory size( B)&amp;lt;/returns&amp;gt;
    public static ulong GetTotalPhys()
    {
        MEMORY_INFO mi = GetMemoryStatus();
        return mi.ullTotalPhys;
    }
    #endregion
}
