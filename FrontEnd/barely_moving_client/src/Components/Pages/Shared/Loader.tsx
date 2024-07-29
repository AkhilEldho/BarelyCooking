import React from 'react'

function Load({type = "warning", size = 100}) {
  return (
    <div
      className={`spinner-border text-${type}`}
      style={{ scale: `${size}%` }} >
        {" "}
    </div>
  )
}

export default Load