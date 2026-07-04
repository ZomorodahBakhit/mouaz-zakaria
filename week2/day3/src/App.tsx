import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Navbar from './components/Navbar/Navbar'
import EmployeeFormModal from './components/EmployeeFormModal/EmployeeFormModal'
import { EmployeeModalContextProvider } from './contexts/EmployeeModalContext'
import About from './pages/About/About'
import EmployeeDetail from './pages/EmployeeDetail/EmployeeDetail'
import EmployeesList from './pages/EmployeesList/EmployeesList'
import './App.scss'

function App() {
  return (
    <EmployeeModalContextProvider>
      <BrowserRouter>
        <div className="app">
          <Navbar />
          <main className="main">
            <Routes>
              <Route path="/" element={<EmployeesList />} />
              <Route path="/employee/:id" element={<EmployeeDetail />} />
              <Route path="/about" element={<About />} />
            </Routes>
          </main>
          <EmployeeFormModal />
        </div>
      </BrowserRouter>
    </EmployeeModalContextProvider>
  )
}

export default App
