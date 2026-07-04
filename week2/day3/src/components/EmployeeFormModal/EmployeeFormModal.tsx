import { useContext, useEffect, useRef, useState } from 'react'
import type { FormEvent } from 'react'
import { Button, Input, InputNumber, Modal, Select, Switch, message } from 'antd'
import type { InputRef } from 'antd'
import { departmentOptions } from '../../data/employeeOptions'
import EmployeeModalContext from '../../contexts/EmployeeModalContext'
import { useAddEmployee } from '../../hooks/useAddEmployee'
import { useUpdateEmployee } from '../../hooks/useUpdateEmployee'
import type { Employee } from '../../types/EmployeeTypes'
import './EmployeeFormModal.scss'

const emptyEmployee: Employee = {
  name: '',
  email: '',
  department: '',
  salary: 0,
  active: false,
}

const getEmployeeErrors = (employee: Employee) => {
  const errors = {
    name: '',
    email: '',
    department: '',
    salary: '',
  }

  if (!employee.name.trim()) {
    errors.name = 'Name is required'
  }

  if (!employee.email.trim()) {
    errors.email = 'Email is required'
  }

  if (!employee.department.trim()) {
    errors.department = 'Department is required'
  }

  if (employee.salary <= 0) {
    errors.salary = 'Salary is required'
  }

  return errors
}

const EmployeeFormModal = () => {
  const nameInputRef = useRef<InputRef>(null)
  const [formData, setFormData] = useState<Employee>(emptyEmployee)
  const [hasSubmitted, setHasSubmitted] = useState(false)
  const [messageApi, contextHolder] = message.useMessage()
  const {
    isEmployeeModalOpen,
    modalMode,
    selectedEmployee,
    closeEmployeeModal,
  } = useContext(EmployeeModalContext)
  const addEmployee = useAddEmployee()
  const updateEmployee = useUpdateEmployee()

  useEffect(() => {
    if (modalMode === 'edit' && selectedEmployee) {
      setFormData({
        ...selectedEmployee,
        active: selectedEmployee.active === true,
      })
      return
    }

    setFormData(emptyEmployee)
  }, [modalMode, selectedEmployee, isEmployeeModalOpen])


  const validationErrors = getEmployeeErrors(formData)
  const isFormValid =
    !validationErrors.name &&
    !validationErrors.email &&
    !validationErrors.department &&
    !validationErrors.salary
  const isSubmitting = addEmployee.isPending || updateEmployee.isPending

  const getFieldStatus = (errorMessage: string) => {
    return hasSubmitted && errorMessage ? 'error' : undefined
  }

  const handleClose = () => {
    setHasSubmitted(false)
    setFormData(emptyEmployee)
    closeEmployeeModal()
  }

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    setHasSubmitted(true)

    if (!isFormValid) {
      return
    }

    if (modalMode === 'edit' && selectedEmployee) {
      if (!selectedEmployee.id) {
        messageApi.error('Employee id is missing')
        return
      }

      updateEmployee.mutate(
        { id: selectedEmployee.id, employee: formData },
        {
          onSuccess: () => {
            messageApi.success('Employee updated')
            handleClose()
          },
          onError: () => {
            messageApi.error('Update failed')
          },
        },
      )
      return
    }

    addEmployee.mutate(formData, {
      onSuccess: () => {
        messageApi.success('Employee added')
        handleClose()
      },
      onError: () => {
        messageApi.error('Add failed')
      },
    })
  }

  return (
    <>
      {contextHolder}
      <Modal
        open={isEmployeeModalOpen}
        title={modalMode === 'create' ? 'Add Employee' : 'Edit Employee'}
        onCancel={handleClose}
        footer={null}
        destroyOnHidden
      >
        <form className="employee-form" onSubmit={handleSubmit}>
          <label className="employee-form__field">
            <span>Name</span>
            <Input
              ref={nameInputRef}
              placeholder="Employee name"
              status={getFieldStatus(validationErrors.name)}
              value={formData.name}
              onChange={(event) =>
                setFormData({ ...formData, name: event.target.value })
              }
            />
            {hasSubmitted && validationErrors.name && (
              <small>{validationErrors.name}</small>
            )}
          </label>

          <label className="employee-form__field">
            <span>Email</span>
            <Input
              placeholder="employee@company.com"
              status={getFieldStatus(validationErrors.email)}
              value={formData.email}
              onChange={(event) =>
                setFormData({ ...formData, email: event.target.value })
              }
            />
            {hasSubmitted && validationErrors.email && (
              <small>{validationErrors.email}</small>
            )}
          </label>

          <label className="employee-form__field">
            <span>Department</span>
            <Select
              options={departmentOptions}
              placeholder="Select department"
              status={getFieldStatus(validationErrors.department)}
              value={formData.department || undefined}
              onChange={(value: string) =>
                setFormData({ ...formData, department: value })
              }
            />
            {hasSubmitted && validationErrors.department && (
              <small>{validationErrors.department}</small>
            )}
          </label>

          <label className="employee-form__field">
            <span>Salary</span>
            <InputNumber
              min={1}
              prefix="$"
              status={getFieldStatus(validationErrors.salary)}
              value={formData.salary}
              onChange={(value) =>
                setFormData({ ...formData, salary: Number(value ?? 0) })
              }
            />
            {hasSubmitted && validationErrors.salary && (
              <small>{validationErrors.salary}</small>
            )}
          </label>

          <label className="employee-form__field">
            <span>Working</span>
            <div className="employee-form__active-row">
              <Switch
                checked={formData.active === true}
                onChange={(checked) =>
                  setFormData({ ...formData, active: checked })
                }
              />
            </div>
          </label>

          <div className="employee-form__actions">
            <Button onClick={handleClose}>Cancel</Button>
            <Button type="primary" htmlType="submit" loading={isSubmitting}>
              {modalMode === 'create' ? 'Add' : 'Save'}
            </Button>
          </div>
        </form>
      </Modal>
    </>
  )
}

export default EmployeeFormModal
