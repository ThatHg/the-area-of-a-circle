using UnityEngine;
using System.Collections;
public class CircleController : MonoBehaviour {
    [SerializeField] private float _amount = 1f;
    [SerializeField] private float _delay = 10f;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _start_radius = 1f;
    private Geometry.Circle _circle;
    private Renderer _renderer;
    private int _added_areas = 0;

    private void Start () {
        _circle = new Geometry.Circle(_start_radius);
        _renderer = GetComponent<Renderer>();
        var m = _renderer.material;
        m.SetFloat("_StartRadius", _start_radius);
        update_shader();
    }

    private void Update () {
        bool update = false;
        if(Input.GetKeyDown(KeyCode.A)) {
            _added_areas--;
            _circle.subtract_area_from_circle(_amount);
            update = true;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            _added_areas++;
            _circle.add_area_to_circle(_amount);
            update = true;
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _circle.add_radius_to_circle(_amount);
            update = true;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            _circle.subtract_radius_from_circle(_amount);
            update = true;
        }

        if(update) {
            update_radius();
        }
    }

    private void update_radius() {
        StopCoroutine("start_updating");
        StartCoroutine("start_updating");
    }

    private IEnumerator start_updating() {
        Vector3 scale = transform.localScale;
        float start_scale = scale.x;
        float end_scale = _circle.get_radius();
        float time = 0;
        while (true) {
            time += Time.deltaTime * _delay;
            float tween_scale = Mathf.Lerp(start_scale, end_scale, _curve.Evaluate(time));
            scale.x = tween_scale;
            scale.y = tween_scale;
            transform.localScale = scale;
            if (time >= 1) {
                scale.x = end_scale;
                scale.y = end_scale;
                transform.localScale = scale;
                update_shader();
                break;
            }
            yield return null;
        }
    }

    private void update_shader() {
        var m = _renderer.material;
        m.SetFloat("_AreaMultiples", _added_areas);
        Debug.Log(_added_areas);
    }
}
