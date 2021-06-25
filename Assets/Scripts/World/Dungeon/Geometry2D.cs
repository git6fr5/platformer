using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry2D
{
    /* --- ENUMS --- */
    public enum Shape {
        ellipse, triangle
    }

    /* --- METHODS --- */
    // constructs the given shape
    public static int[][] ConstructShape(Shape shape, int vertical, int horizontal) {
        switch (shape) {
            case Shape.ellipse:
                Debug.Log("Constructing Ellipse");
                return Ellipse(vertical, horizontal);
            case Shape.triangle:
                Debug.Log("Constructing Triangle");
                return Triangle(vertical, horizontal);
            default:
                Debug.Log("Unknown Shape");
                return new int[0][];
        }
    }

    // creates an ellipse subgrid
    public static int[][] Ellipse(int vertical, int horizontal) {
        // initialize the grid
        int[][] ellipse = new int[vertical][];
        for (int i = 0; i < ellipse.Length; i++) {
            ellipse[i] = new int[horizontal];
        }
        // the major and minor axis radii
        float a = (float)horizontal / 2;
        float b = (float)vertical / 2;
        // draw the ellipse grid
        for (int i = 0; i < vertical; i++) {
            for (int j = 0; j < horizontal; j++) {
                float x = (float)j - a;
                float y = (float)i - b;
                float ellipticalBoundary = (x * x / (a * a)) + (y * y / (b * b));
                if (Mathf.Abs(ellipticalBoundary) < 1) { ellipse[i][j] = 1; }
            }
        }
        return ellipse;
    }

    // creates a triangle sub grid
    public static int[][] Triangle(int vertical, int horizontal)
    {
        // initialize the grid
        int[][] triangle = new int[vertical][];
        for (int i = 0; i < triangle.Length; i++) {
            triangle[i] = new int[horizontal];
        }
        // the side lengths
        float center = (int)(horizontal / 3);
        float ratioA = (float)vertical / center;
        float ratioB = (float)vertical / (horizontal - center);
        // draw the triangle
        for (int i = 0; i < vertical; i++) {
            for (int j = 0; j < horizontal; j++) {
                if (j < center && (vertical - i) < ratioA * j) { triangle[i][j] = 1; }
                if (j >= center && i > ratioB * j + center) { triangle[i][j] = 1; }
            }
        }      
        return triangle;
    }

    // draws the boundaries of a shape
    void Boundaries(int[][] shape) {
        for (int i = 0; i < shape.Length; i++) {
            for (int j = 0; j < shape[0].Length; j++) {

            }
        }
    }

    // draws the corners of a shape
    void Corners(int[][] shape) { 
        for (int i = 0; i < shape.Length; i++) {
            for (int j = 0; j < shape[0].Length; j++) {

            }
        }
    }
}
