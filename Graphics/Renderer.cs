using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;
//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;

namespace Graphics
{
    class Renderer
    {
        Shader sh;

        uint dolfineBufferID;
        uint xyzAxesBufferID;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;

        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 1f;
        float rotationAngle = 0;

        public float translationX = 0,
                     translationY = 0,
                     translationZ = 0;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 dolfineCenter;

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
                 //Background Color
            Gl.glClearColor(0.843f, 0.925f, 0.929f, 0.3f);

            float[] dolfineVertices = { 
                //At each Traiangle pionts is followed by its Color
                //polygon 1
                 0.847f*30,-0.659f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.818f*30,-0.767f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.659f*30,-0.55f*30,-5.0f,
                              0.145f,0.588f,0.745f ,
                 0.741f*30,-0.488f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                  //T1
                 0.659f*30,-0.55f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.741f*30,-0.488f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.676f*30,-0.209f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                  //T3
                 0.676f*30,-0.209f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.559f*30,-0.318f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.559f*30,-0.38f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.659f*30,-0.55f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                 //T4
                 0.559f*30,-0.318f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.676f*30,-0.209f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.388f*30,0.178f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.512f*30,-0.318f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                   //T5
                 0.388f*30,0.178f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.512f*30,-0.318f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.506f*30,-0.38f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.282f*30,-0.039f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                   //T6
                 0.506f*30,-0.38f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.282f*30,-0.039f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.359f*30,-0.473f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                   //T7
                 0.506f*30,-0.38f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.359f*30,-0.473f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.535f*30,-0.519f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.535f*30,-0.426f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                   //T8
                 0.535f*30,-0.519f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.535f*30,-0.426f*30,-5.0f,
                 0.145f,0.588f,0.745f ,
                 0.559f*30,-0.38f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.659f*30,-0.55f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                   //T9
                 0.359f*30,-0.473f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.659f*30,-0.55f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.659f*30,-0.659f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                   //T10
                 0.659f*30,-0.55f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.659f*30,-0.659f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.818f*30,-0.767f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                   //T11
                 0.359f*30,-0.473f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.659f*30,-0.659f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.3f*30,-0.612f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                   //T12
                 0.388f*30,0.178f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.282f*30,-0.039f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.076f*30,0.349f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                  //T13
                 0.076f*30,0.349f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.282f*30,-0.039f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.059f*30,-0.116f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                  //T14
                -0.059f*30,-0.116f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.282f*30,-0.039f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.241f*30,-0.457f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                  //T15
                 0.241f*30,-0.457f*30,-5.0f,
                              0.772f,0.898f,0.925f ,
                -0.059f*30,-0.116f*30,-5.0f,
                              0.772f,0.898f,0.925f ,
                -0.294f*30,-0.395f*30,-5.0f,
                              0.772f,0.898f,0.925f*30, 
                  //T16
                -0.294f*30,-0.395f*30,-5.0f,
                               0.772f,0.898f,0.925f ,
                 0.241f*30,-0.457f*30,-5.0f,
                                0.772f,0.898f,0.925f*30,
                 0.176f*30,-0.597f*30,-5.0f,
                               0.772f,0.898f,0.925f , 
                  //T17
                 0.282f*30,-0.039f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.241f*30,-0.457f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.359f*30,-0.473f*30,-5.0f,
                0.145f,0.588f,0.745f , 
                  //T18
                 0.241f*30,-0.457f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.359f*30,-0.473f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.118f*30,-0.674f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                  //T19
                 0.359f*30,-0.473f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.118f*30,-0.674f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.194f*30,-0.845f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                  //T20
                 0.194f*30,-0.845f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                 0.118f*30,-0.674f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.047f*30,-0.845f*30,-5.0f,
                                0.145f,0.588f,0.745f , 
                  //T21
                -0.059f*30,-0.116f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                 0.076f*30,0.349f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                                -0.453f*30,-0.008f*30,-5.0f,
                0.145f,0.588f,0.745f , 
                 //T22
                 0.076f*30,0.349f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.453f*30,-0.008f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.141f*30,0.426f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T23
                 0.076f*30,0.349f*30,-5.0f,
                                0.145f,0.588f,0.745f ,
                -0.141f*30,0.426f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.1f*30,0.721f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T24
                -0.1f*30,0.721f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.141f*30,0.426f*30,-5.0f,
                              0.145f,0.588f,0.745f ,
                -0.188f*30,0.674f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T25
                -0.188f*30,0.674f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.1f*30,0.721f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.2f*30,0.783f*30,-5.0f,
                              0.145f,0.588f,0.745f , 
                 //T26
                -0.059f*30,-0.116f*30,-5.0f,
                              0.145f,0.588f,0.745f ,
                -0.453f*30,-0.008f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.294f*30,-0.395f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T27 
                -0.453f*30,-0.008f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.294f*30,-0.395f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.571f*30,-0.612f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T28 
                -0.571f*30,-0.612f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.453f*30,-0.008f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.671f*30,-0.488f*30,-5.0f,//import
                               0.145f,0.588f,0.745f , 
                 //T29
                -0.671f*30,-0.488f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.571f*30,-0.612f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.676f*30,-0.628f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T30
                -0.676f*30,-0.628f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.671f*30,-0.488f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.724f*30,-0.488f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T31
                -0.671f*30,-0.488f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.7f*30,-0.349f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.794f*30,-0.535f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                 //T32 
                -0.724f*30,-0.488f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.794f*30,-0.535f*30,-5.0f,
                              0.145f,0.588f,0.745f ,
                -0.788f*30,-0.69f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.676f*30,-0.628f*30,-5.0f,
                               0.145f,0.588f,0.745f , 
                //T33
                -0.794f*30,-0.535f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.788f*30,-0.69f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
                -0.947f*30,-0.659f*30,-5.0f,
                               0.145f,0.588f,0.745f ,
            }; 

            dolfineCenter = new vec3(2.07f, 2.4f, -5);
            float[] xyzAxesVertices = {
            //x
            0.0f, 0.0f, 0.0f,
                        0.5f, 0.5f, 0.5f,
            100.0f, 0.0f, 0.0f,
                        0.5f, 0.5f, 0.5f,
            //y
              0.0f, 0.0f, 0.0f,
                       0.0f,1.0f, 0.0f,
            0.0f, 100.0f, 0.0f,
                      0.0f, 1.0f, 0.0f, 
            //z
              0.0f, 0.0f, 0.0f,
                          0.5f, 0.5f, 0.5f,
            0.0f, 0.0f, -100.0f,
                         0.5f, 0.5f, 0.5f,
            };


            dolfineBufferID = GPU.GenerateBuffer(dolfineVertices);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);

            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(50, 50, 50), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 1, 0)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);

            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

            timer.Start();
        }

        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            #region XYZ axis

            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, 1);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 1, 3);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 3, 5);


            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region Animated Triangle
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, dolfineBufferID);
            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 4, 3);
            Gl.glDrawArrays(Gl.GL_POLYGON, 7, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 11, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 15, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 19, 3);
            Gl.glDrawArrays(Gl.GL_POLYGON, 22, 4);
            Gl.glDrawArrays(Gl.GL_POLYGON, 26, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 30, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 33, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 36, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 39, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 42, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 45, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 48, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 51, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 54, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 57, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 60, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 63, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 66, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 69, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 72, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 75, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 78, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 81, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 84, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 87, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 90, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 93, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 96, 3);
            Gl.glDrawArrays(Gl.GL_POLYGON, 99, 4);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 103, 3);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion

        }


        public void Update()
        {

            timer.Stop();
            var deltaTime = timer.ElapsedMilliseconds / 1000.0f;

            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
            transformations.Add(glm.translate(new mat4(1), -1 * dolfineCenter));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 0, 1)));
            transformations.Add(glm.translate(new mat4(1), dolfineCenter));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            ModelMatrix = MathHelper.MultiplyMatrices(transformations);

             timer.Reset();
             timer.Start();
        }

        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}