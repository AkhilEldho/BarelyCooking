import React from 'react'
import { Link } from 'react-router-dom';
import { cakeModel } from '../../../Interface';

interface Props {
    cake: cakeModel;
}

function CakeCard(props: Props) {
  return (
    <div className="col-md-4 col-12 p-4">
      <div className="card" style={{ boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)" }}>
        <div className="card-body pt-2">
          <div className="row col-10 offset-1 p-4">
            <Link to={`/cake/${props.cake.cakeId}`}>
              <img
                src={props.cake.imageFile}
                alt=""
                className="fixed-size-image mt-5"
              />
            </Link>
          </div>

          {props.cake.specialTags && props.cake.specialTags.length > 0 && (
            <i
              className="bi bi-star btn btn-success"
              style={{
                position: "absolute",
                top: "15px",
                left: "15px",
                padding: "5px 10px",
                borderRadius: "3px",
                outline: "none !important",
                cursor: "pointer",
              }}
            >
              &nbsp; {props.cake.specialTags}
            </i>
          )}

          <i
            className="bi bi-cart-plus btn btn-outline-danger"
            style={{
              position: "absolute",
              top: "15px",
              right: "15px",
              padding: "5px 10px",
              borderRadius: "3px",
              outline: "none !important",
              cursor: "pointer",
            }}
          ></i>

          <div className="text-center">
            <p className="card-title m-0 text-success fs-3">
              <Link to={`/cake/${props.cake.cakeId}`}
                style={{ textDecoration: "none", color: 'darkblue' }}>
                {props.cake.name}
              </Link>
            </p>
            <p className="badge bg-secondary" style={{ fontSize: "12px" }}>
              {props.cake.category}
            </p>
          </div>
          <div className="row text-center">
            <h4>${props.cake.price}</h4>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CakeCard;
