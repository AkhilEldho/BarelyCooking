import React, { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useGetCakeQuery } from '../API/cakeApi';

function CakeDetails() {
  const { cakeId } = useParams();
  const { data, isLoading } = useGetCakeQuery(cakeId);
  const navigate = useNavigate();
  const [quantity, setQuantity] = useState(1);
  console.log(data);

  const handleQuantity = (counter: number) => {
    let newQuantity = quantity + counter;

    if (newQuantity === 0) {
      newQuantity = 1;
    }

    setQuantity(newQuantity);
    return;
  }

  return (
    <div className="container pt-4 pt-md-5">
      {!isLoading ? (
        <div className="row">
          <div className="col-7">
            <h2 className="text-success">{data.result?.name}</h2>
            <span>
              <span
                className="badge text-bg-dark pt-2"
                style={{ height: "40px", fontSize: "20px" }}
              >
                {data.result?.category}
              </span>
            </span>
            <span>
              <span
                className="badge text-bg-light pt-2"
                style={{ height: "40px", fontSize: "20px" }}
              >
                {data.result?.specialTags}
              </span>
            </span>
            <p style={{ fontSize: "20px" }} className="pt-2">
              {data.result?.description}
            </p>
            <span className="h3">${data.result?.price}</span> &nbsp;&nbsp;&nbsp;
            <span
              className="pb-2  p-3"
              style={{ border: "1px solid #333", borderRadius: "30px" }}
            >
              <i
                onClick={() => {
                  handleQuantity(-1);
                }}
                className="bi bi-dash p-1"
                style={{ fontSize: "25px", cursor: "pointer" }}
              ></i>
              <span className="h3 mt-3 px-3">{quantity}</span>
              <i
                onClick={() => {
                  handleQuantity(+1);
                }}
                className="bi bi-plus p-1"
                style={{ fontSize: "25px", cursor: "pointer" }}
              ></i>
            </span>
            <div className="row pt-4">
              <div className="col-5">
                <button className="btn btn-success form-control">
                  Add to Cart
                </button>
              </div>
              <div className="col-5 ">
                <button className="btn btn-secondary form-control"
                  onClick={() => navigate(-1)}
                >
                  Back to Home
                </button>
              </div>
            </div>
          </div>
          <div className="col-5">
            <img
              src={data.result?.imageFile}
              width="100%"
              style={{ borderRadius: "50%", height: "auto", width: "100%", objectFit: "cover" }}
              alt="No content"
            ></img>
          </div>
        </div>
      ) : (
        <div
          className="d-flex justify-content-center"
          style={{ width: "100%" }}>
          <div>Loading ...</div>
        </div>
      )}
    </div>
  )
}

export default CakeDetails;
