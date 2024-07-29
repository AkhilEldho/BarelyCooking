import React from 'react';

function MainLoader() {
  return (
    <div
      style={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh', // Full viewport height
        backgroundColor: 'rgba(0, 0, 0, 0.1)', // Light background overlay
      }}
    >
      <div
        className="spinner-border text-warning"
        style={{
          width: '3rem',
          height: '3rem',
        }}
      ></div>
    </div>
  );
}

export default MainLoader;
