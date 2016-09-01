using UnityEngine;
namespace Geometry {
    public class Circle {
        private float _area;
        private float _radius;
        public Circle (float radius) {
            set_radius(radius);
        }

        public void add_area_to_circle(float area) {
            set_area(_area + area);
        }

        public void add_radius_to_circle(float radius) {
            set_radius(_radius + radius);
        }

        public void subtract_area_from_circle(float area) {
            set_area(_area - area);
        }

        public void subtract_radius_from_circle(float radius) {
            set_radius(_radius - radius);
        }

        public float get_radius() {
            return _radius;
        }

        public float get_area() {
            return _area;
        }

        public void set_radius(float radius) {
            _area = calculate_area(_radius = radius);
        }

        public void set_area(float area) {
            _radius = calculate_radius(_area = area);
        }

        private float calculate_radius(float area) {
            return (Mathf.Sqrt(area / Mathf.PI));
        }

        private float calculate_area(float radius) {
            return (Mathf.PI * radius * radius);
        }
    }
}

