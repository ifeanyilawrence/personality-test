import React from 'react';
import { Routes, Route } from "react-router-dom";

import { Home } from './pages/home';
import { PageContainer } from './pages/page-container';
import { Question } from './pages/question/index';
import { Result } from './pages/result';
import { Provider } from './store/index';

function App() {
  return (
    <PageContainer>
      <Provider>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/question" element={<Question />} />
          <Route path="/result" element={<Result />} />
          <Route path="*" element={<Home />} />
        </Routes>
      </Provider>
    </PageContainer>
  );
}

export default App;
