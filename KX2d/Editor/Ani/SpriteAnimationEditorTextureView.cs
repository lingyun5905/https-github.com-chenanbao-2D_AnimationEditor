﻿using UnityEditor;
using UnityEngine;

namespace KX2d.Editor.Ani
{
    public class SpriteAnimationEditorTextureView
    {
       
        private float editorDisplayScale = 1f;
        private Vector2 textureScrollPos = new Vector2(0.0f, 0.0f);
        private int textureBorderPixels = 0;
        public Texture2D CurTexture;

        public SpriteAnimationEditorTextureView()
        {
            
        }

        public void Draw()
        {



            if (editorDisplayScale <= 1.0f) editorDisplayScale = 1.0f;
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            Rect rect = GUILayoutUtility.GetRect(128.0f, 128.0f, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            TextureGrid.Draw(rect);

            Handles.color = new Color(1, 1, 0, 0.5f);
            Handles.DrawLine(new Vector2(rect.x, rect.center.y), new Vector2(rect.x + rect.width, rect.center.y));
            Handles.DrawLine(new Vector2(rect.center.x, rect.y), new Vector2(rect.center.x, rect.y + rect.height));

            if (CurTexture != null)
            {
                // middle mouse drag and scroll zoom
                if (rect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.MouseDrag && Event.current.button == 2)
                    {
                        textureScrollPos -= Event.current.delta * editorDisplayScale;
                        Event.current.Use();
                        HandleUtility.Repaint();
                    }
                    if (Event.current.type == EventType.ScrollWheel)
                    {
                        editorDisplayScale -= Event.current.delta.y * 0.03f;
                        Event.current.Use();
                        HandleUtility.Repaint();
                    }
                }

                bool alphaBlend = true;
                textureScrollPos = GUI.BeginScrollView(rect, textureScrollPos,
                    new Rect(0, 0, textureBorderPixels * 2 + (CurTexture.width) * editorDisplayScale, textureBorderPixels * 2 + (CurTexture.height) * editorDisplayScale));
                Rect textureRect = new Rect(textureBorderPixels, textureBorderPixels, CurTexture.width * editorDisplayScale, CurTexture.height * editorDisplayScale);
                textureRect.x += (rect.width - textureRect.width)/2;
                textureRect.y += (rect.height - textureRect.height) / 2;
                CurTexture.filterMode = FilterMode.Point;
                GUI.DrawTexture(textureRect, CurTexture, ScaleMode.ScaleAndCrop, alphaBlend);
                
                GUI.EndScrollView();

               
            }
            GUILayout.EndVertical();
        }


    }
}
