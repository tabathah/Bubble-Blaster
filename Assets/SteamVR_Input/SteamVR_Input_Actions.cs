//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Action_Boolean p_default_Grab;
        
        private static SteamVR_Action_Boolean p_default_Teleport;
        
        private static SteamVR_Action_Pose p_default_Pose;
        
        private static SteamVR_Action_Boolean p_default_Grip;
        
        private static SteamVR_Action_Boolean p_default_Swap;
        
        public static SteamVR_Action_Boolean default_Grab
        {
            get
            {
                return SteamVR_Actions.p_default_Grab.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Teleport
        {
            get
            {
                return SteamVR_Actions.p_default_Teleport.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose default_Pose
        {
            get
            {
                return SteamVR_Actions.p_default_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Grip
        {
            get
            {
                return SteamVR_Actions.p_default_Grip.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean default_Swap
        {
            get
            {
                return SteamVR_Actions.p_default_Swap.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        private static void InitializeActionArrays()
        {
            Valve.VR.SteamVR_Input.actions = new Valve.VR.SteamVR_Action[] {
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Pose,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_Swap};
            Valve.VR.SteamVR_Input.actionsIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Pose,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_Swap};
            Valve.VR.SteamVR_Input.actionsOut = new Valve.VR.ISteamVR_Action_Out[0];
            Valve.VR.SteamVR_Input.actionsVibration = new Valve.VR.SteamVR_Action_Vibration[0];
            Valve.VR.SteamVR_Input.actionsPose = new Valve.VR.SteamVR_Action_Pose[] {
                    SteamVR_Actions.default_Pose};
            Valve.VR.SteamVR_Input.actionsBoolean = new Valve.VR.SteamVR_Action_Boolean[] {
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_Swap};
            Valve.VR.SteamVR_Input.actionsSingle = new Valve.VR.SteamVR_Action_Single[0];
            Valve.VR.SteamVR_Input.actionsVector2 = new Valve.VR.SteamVR_Action_Vector2[0];
            Valve.VR.SteamVR_Input.actionsVector3 = new Valve.VR.SteamVR_Action_Vector3[0];
            Valve.VR.SteamVR_Input.actionsSkeleton = new Valve.VR.SteamVR_Action_Skeleton[0];
            Valve.VR.SteamVR_Input.actionsNonPoseNonSkeletonIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.default_Grab,
                    SteamVR_Actions.default_Teleport,
                    SteamVR_Actions.default_Grip,
                    SteamVR_Actions.default_Swap};
        }
        
        private static void PreInitActions()
        {
            SteamVR_Actions.p_default_Grab = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Grab")));
            SteamVR_Actions.p_default_Teleport = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Teleport")));
            SteamVR_Actions.p_default_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/default/in/Pose")));
            SteamVR_Actions.p_default_Grip = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Grip")));
            SteamVR_Actions.p_default_Swap = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/default/in/Swap")));
        }
    }
}
