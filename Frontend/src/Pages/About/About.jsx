import React, { useEffect, useRef } from 'react';
import Style from './About.module.css';

import ReactFullpage from '@fullpage/react-fullpage';

const ScrollSnap = ({ children }) => {
  // Only show the Fullpage component if there are more than one child
  if (children.length <= 1) return children;

  return (
    <ReactFullpage
      scrollOverflow={true}
      render={({ state, fullpageApi }) => {
        return (
          <div id='fullpage-wrapper'>
            {React.Children.map(children, (child, index) => (
              <div key={index} className='section'>
                {child}
              </div>
            ))}
          </div>
        );
      }}
    />
  );
};

const DummyComponent = ({ id }) => {
  const styles = {
    height: '100vh',
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    fontSize: '3rem',
    color: 'white',
    background: `hsl(${id * 100}, 70%, 50%)`, // Different color for each section
  };

  return <div style={styles}>This is section {id}</div>;
};

export default function About() {
  return (
    <ScrollSnap>
      <DummyComponent id={1} />
      <DummyComponent id={2} />
      <DummyComponent id={3} />
      <DummyComponent id={4} />
    </ScrollSnap>
  );
}
